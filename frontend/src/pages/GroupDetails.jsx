import { useParams, Link } from 'react-router';
import { useEffect, useState } from 'react';
import Container from 'react-bootstrap/Container';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Breadcrumb from 'react-bootstrap/Breadcrumb';
import Button from 'react-bootstrap/Button';
import Table from 'react-bootstrap/Table';
import Modal from 'react-bootstrap/Modal';
import Form from 'react-bootstrap/Form';
import Badge from 'react-bootstrap/Badge';
import Tab from 'react-bootstrap/Tab';
import Tabs from 'react-bootstrap/Tabs';
import { Gear, PersonPlus, XCircle, PlusCircle, CashCoin } from 'react-bootstrap-icons';
import { useNavigate } from "react-router";

export default function Group() {
	const { groupId } = useParams();
	const [groupName, setGroupName] = useState('');
	const [members, setMembers] = useState([]);
	const [transactions, setTransactions] = useState([]);
	const [showAddMemberModal, setShowAddMemberModal] = useState(false);
	const [showSettleModal, setShowSettleModal] = useState(false);
	const [selectedMember, setSelectedMember] = useState(null);
	const [activeTab, setActiveTab] = useState('members');
	const [selfMember, setSelfMember] = useState(null);

	const navigate = useNavigate();
	
	useEffect(() => {
		fetch(`/api/groups/${groupId}`)
			.then(res => res.json())
			.then(data => {
				setGroupName(data.name);
			});
	}, [groupId]);

	useEffect(() => {
		if (activeTab === 'members') {
			fetch(`/api/groups/${groupId}/members`)
				.then(res => res.json())
				.then(data => {
					setMembers(data);
					const self = data.find(m => m.isSelf);
					if (self) {
  						setSelfMember(self);
					}
					console.log(self);
				});
		}
	}, [groupId, activeTab]);

	useEffect(() => {
		if (activeTab === 'transactions') {
			fetch(`/api/groups/${groupId}/transactions`)
				.then(res => res.json())
				.then(data => {
					setTransactions(data);
				});
		}
	}, [groupId, activeTab]);

	const handleOpenSettleModal = (member) => {
		setSelectedMember(member);
		setShowSettleModal(true);
	};
	function handleAddTransaction(){
		navigate(`/group/${groupId}/add-transaction`);
	}
    function handleAddMember(e){
        e.preventDefault();
        const formData = new FormData(e.target);
        const memberName = formData.get('name');
        fetch(`/api/groups/${groupId}/members`, {
            method:"POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body:JSON.stringify({name: memberName})
        })
        .then(res => res.json())
        .then(newMember =>{
            setShowAddMemberModal(false);
            setMembers(prev => [...prev, newMember]);
        })
        .catch((e)=>{
            console.log(`API error: ${e}`);
        });
    }

	function handleSettlement(e){
		e.preventDefault();
		const payer = new FormData(e.target).get("payer");
		const payee = payer == selfMember.id ? selectedMember : selfMember;
		fetch(`/api/groups/${groupId}/transactions`, {
            method:"POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: "Settlement",
                payerMemberId: +payer,
                fullAmount: Math.abs(selectedMember.balance),
                splitType: "exact",
                shares: [
					{
						memberId: payee.id,
						amount: Math.abs(selectedMember.balance)
					}
				]
            })
            })
            .then(res => {
		if (!res.ok) throw new Error('Failed to settle');
		return res.json();
	})
	.then(() => {
		setShowSettleModal(false);
		return fetch(`/api/groups/${groupId}/members`);
	})
	.then(res => res.json())
	.then(data => {
		setMembers(data);
	})
	}

	function handleRemoveMember(id){
		fetch(`/api/members/${id}`, {
			method: "DELETE"
		})
		.then(res => {
			if (!res.ok) throw new Error('Failed to delete member');
			return null;
		})
		.then(()=>{
			setMembers(prev => prev.filter(member => member.id !== id));
		});

	}

	return (
		<Container>
			<Row className="mb-3">
				<Col>
					<Breadcrumb>
						<Breadcrumb.Item href="/home">Home</Breadcrumb.Item>
						<Breadcrumb.Item active>{groupName}</Breadcrumb.Item>
					</Breadcrumb>
				</Col>
			</Row>
			<Row className="mb-3">
				<Col className="d-flex justify-content-between">
					<h1>{groupName}</h1>
					{/* <Button variant="outline-secondary">
						<Gear /> Settings
					</Button> */}
				</Col>
			</Row>
			<Tabs
				activeKey={activeTab}
				onSelect={(k) => setActiveTab(k)}
				className="mb-3"
			>
				<Tab eventKey="members" title="Members">
					<div className="d-flex justify-content-end mb-3">
						<Button 
							variant="primary" 
							onClick={() => setShowAddMemberModal(true)}
						>
							<PersonPlus className="me-1" /> Add Member
						</Button>
					</div>
					
					<Table responsive>
						<thead>
							<tr>
								<th>Name</th>
								<th>Status</th>
								<th>Actions</th>
							</tr>
						</thead>
						<tbody>
							{members.filter(m => !m.isSelf).map(member => (
								<tr key={member.id}>
									<td>{member.name}</td>
									<td>
										{member.balance.toFixed(2) > 0 ? (
											<Badge bg="success">You are owed €{Math.abs(member.balance).toFixed(2)}</Badge>
										) : member.balance.toFixed(2) < 0 ? (
											<Badge bg="danger">You owe €{Math.abs(member.balance).toFixed(2)}</Badge>
										) : (
											<Badge bg="secondary">Settled</Badge>
										)}
									</td>
									<td>
										{member.balance.toFixed(2) != 0 ? (
											<Button 
												variant="outline-primary" 
												size="sm"
												onClick={() => handleOpenSettleModal(member)}
											>
												<CashCoin /> Settle
											</Button>
										) : (
											<Button onClick={()=>handleRemoveMember(member.id)}
												variant="outline-danger" 
												size="sm"
											>
												<XCircle /> Remove
											</Button>
										)}
									</td>
								</tr>
							))}
						</tbody>
					</Table>
				</Tab>
				<Tab eventKey="transactions" title="Transactions">
					<div className="d-flex justify-content-end mb-3">
						<Button
							onClick={handleAddTransaction}
						>
							<PlusCircle className="me-1" /> New Transaction
						</Button>
					</div>
					
					<Table responsive>
						<thead>
							<tr>
								<th>Title</th>
								<th>Paid By</th>
								<th>Amount</th>
								<th>Split Among</th>
							</tr>
						</thead>
						<tbody>
							{transactions.map(transaction => (
								<tr key={transaction.id}>
									<td>{transaction.title}</td> 
									<td>{transaction.payerName}</td>
									<td>€{transaction.fullAmount.toFixed(2)}</td>
									<td>
									{transaction.shares.map((a)=>{
										return a.memberName;
									}).join(', ')}
									</td>
								</tr>
							))}
						</tbody>
					</Table>
				</Tab>
			</Tabs>
			<Modal show={showAddMemberModal} onHide={() => setShowAddMemberModal(false)}>
                <Form onSubmit={handleAddMember}>
				<Modal.Header closeButton>
					<Modal.Title>Add New Member</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					<Form.Group className="mb-3">
						<Form.Label>Member Name</Form.Label>
						<Form.Control 
							type="text" 
							placeholder="Enter name"
                            name="name"
						/>
					</Form.Group>
				</Modal.Body>
				<Modal.Footer>
					<Button variant="secondary" onClick={() => setShowAddMemberModal(false)}>
						Cancel
					</Button>
					<Button type="submit">
						Add Member
					</Button>
				</Modal.Footer>
                </Form>
			</Modal>
			<Modal show={showSettleModal} onHide={() => setShowSettleModal(false)}>
				<Form onSubmit={handleSettlement}>
				<Modal.Header closeButton>
					<Modal.Title>
						Settle with {selectedMember ? selectedMember.name : ''}
					</Modal.Title>
				</Modal.Header>
				<Modal.Body>
					{selectedMember && (
						<>
							<div className="text-center mb-3">
								{selectedMember.balance > 0 ? (
									<Badge bg="success" className="p-2 fs-5">
										{selectedMember.name} owes you €{Math.abs(selectedMember.balance).toFixed(2)}
									</Badge>
								) : (
									<Badge bg="danger" className="p-2 fs-5">
										You owe {selectedMember.name} €{Math.abs(selectedMember.balance).toFixed(2)}
									</Badge>
								)}
							</div>
							<Form.Group className="mb-3">
								<Form.Label>Payment method</Form.Label>
								<Form.Select>
									<option>Cash</option>
									<option>Bank Transfer</option>
									<option>Other</option>
								</Form.Select>
							</Form.Group>
						</>
					)}
				</Modal.Body>
				<Modal.Footer>
					<Button variant="secondary" onClick={() => setShowSettleModal(false)}>
						Cancel
					</Button>
					<Button variant="success" type="submit">
						Record Settlement
					</Button>
				</Modal.Footer>
				{selectedMember !== null && <input name="payer" type="hidden" value={selectedMember.balance > 0 ? selectedMember.id : selfMember.id}></input>}
				</Form>
			</Modal>
		</Container>
	);
}