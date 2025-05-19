import { useParams, Link } from 'react-router';
import { useEffect, useState } from 'react';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Breadcrumb from 'react-bootstrap/Breadcrumb';
import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import Container from 'react-bootstrap/esm/Container';
import { useNavigate } from "react-router";
import InputGroup from 'react-bootstrap/InputGroup';

export default function Group() {
    const { groupId } = useParams();
    const [groupName, setGroupName] = useState('');
    const [members, setMembers] = useState([]);
    const [splitType, setSplitType] = useState("equal");
    const [shares, setShares] = useState(null);
    const navigate = useNavigate();

    useEffect(() => {
		fetch(`/api/groups/${groupId}`)
			.then(res => res.json())
			.then(data => {
				setGroupName(data.name);
		});
	}, [groupId]);

    useEffect(() => {
		fetch(`/api/groups/${groupId}/members`)
			.then(res => res.json())
			.then(data => {
				setMembers(data);
		});
	}, [groupId]);

    function handleAddTransaction(e){
        e.preventDefault();
        const formData = new FormData(e.target);
        const title = formData.get('title');
        const memberId = formData.get('payerMember');
        const fullAmount = formData.get('fullAmount');
        let transShares = [];
        if(splitType != "equal" && shares != null){
            transShares = Object.entries(shares).map(([memberId, amount]) => ({
                    memberId: +memberId,
                    amount
                }));
        }
        fetch(`/api/groups/${groupId}/transactions`, {
            method:"POST",
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                title: title,
                payerMemberId: +memberId,
                fullAmount: +fullAmount,
                splitType: splitType,
                shares: transShares
            })
            })
            .then()
			.then(navigate(`/group/${groupId}`));
    }

    return (
    <>
        <Row className="mb-3">
            <Col>
              <Breadcrumb>
                    <Breadcrumb.Item href="/home">Home</Breadcrumb.Item>
					<Breadcrumb.Item href={"/group/"+groupId}>{groupName}</Breadcrumb.Item>
                    <Breadcrumb.Item active>New transaction</Breadcrumb.Item>
              </Breadcrumb>
            </Col>
        </Row>
        <Container className="m-5 p-5 mb-0 pb-0 pt-0">
        <Row className="justify-content-center">
            <Col xs={8}>
                <Form onSubmit={handleAddTransaction}>
                    <Row>
                        <Col>
                            <Form.Group className="mb-3">
                                <Form.Label className="fw-bold">Amount</Form.Label>
                                <InputGroup>
                                    <InputGroup.Text>€</InputGroup.Text>
            	    	            <Form.Control name="fullAmount" type="number" placeholder="0.00" min="0.01" step="0.01" />
                                </InputGroup>
            	            </Form.Group>
                        </Col>
            	        <Col>
                            <Form.Group className="mb-3">
            	    	        <Form.Label className="fw-bold">Paid by</Form.Label>
            	    	        <Form.Select name="payerMember">
            	    		        <option value="">Select a member</option>
            	    		        {members.map(member => (
            	    		        	<option key={member.id} value={member.id}>
            	    		        		{member.name}
            	    		        	</option>
            	    		        ))}
            	    	        </Form.Select>
            	            </Form.Group>
                        </Col>
                    </Row>
                    <Form.Group className="mb-3 fw-bold">
            	    	<Form.Label>Description</Form.Label>
            	    	<Form.Control name="title" className="w-50"type="text" placeholder="What was this for?" />
            	    </Form.Group>

            	    {/* <Form.Group className="mb-3">
            	    	<Form.Label>Date</Form.Label>
            	    	<Form.Control type="date" />
            	    </Form.Group> */}
            	    <Form.Group className="mb-3">
                        <Row className="mb-4">
                            <Col xs="auto" className="d-flex align-items-center">
            	    	        <Form.Label className="mb-0 fw-bold">Split</Form.Label>
                            </Col>
                            <Col xs="auto">
                                <Form.Select placeholder="equal" onChange={e => setSplitType(e.target.value)}>
            	    	        	<option value="equal">Split equally</option>
            	    	        	<option value="percent">Split by percentage</option>
            	    	        	<option value="exact">Split by exact amounts</option>
            	    	        </Form.Select>
                            </Col>
                        </Row>
                        {splitType !== "equal" && 
            	            members.map(member => (
                                <Form.Group key={member.id}>
                                    <Form.Label>{member.name}</Form.Label>
                                    <InputGroup className="w-50">
                                        {splitType == "exact" && <InputGroup.Text>€</InputGroup.Text>}
                                        {splitType == "percent" && <InputGroup.Text>%</InputGroup.Text>}
                                        <Form.Control type="number" placeholder="0.00"
                                        onChange={e => setShares(s => ({
                                            ...s,
                                            [member.id]: parseFloat(e.target.value) || 0
                                        }))}
                                        ></Form.Control>
                                    </InputGroup>
                                </Form.Group>
            	    	    ))
                        }
            	    </Form.Group>
                    <Container className="d-flex justify-content-center">
                        <Button type="submit" className="w-25" >
						    Create transaction
					    </Button>
                    </Container>
                </Form>
            </Col>
        </Row>
        </Container>
    </>
    );
}