import Container from 'react-bootstrap/Container';
import Breadcrumb from 'react-bootstrap/Breadcrumb';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import { useEffect, useState } from 'react';
import Button from 'react-bootstrap/Button';
import { useNavigate } from "react-router";
import Badge from 'react-bootstrap/Badge';

export default function Home() {
  const navigate = useNavigate(); 
  const [groups, setGroups] = useState([]);

  useEffect(() => {
    fetch('/api/groups')
      .then((res) => {
        return res.json();
      })
      .then((data) =>{
        setGroups(data);
      })
      .catch((e)=>{
        console.log(`API error: ${e}`)
      })
  }, []);

  function handleAddClick(){
    navigate("/add-group");
  }
  function handleGroupClick(groupId){
    navigate(`/group/${groupId}`);
  }
  return (
    <>
      <Row className="mb-3">
        <Col>
          <Breadcrumb>
            <Breadcrumb.Item active>Home</Breadcrumb.Item>
          </Breadcrumb>
        </Col>
      </Row>
      <Row>
        <Col>
          {groups.map((group)=>(
            <Container key={group.id} className="group rounded-4 bg-white p-3 m-4" onClick={()=>handleGroupClick(group.id)}>
              <Container>
                <h5 className="fw-bold">{group.name}</h5>
                {group.balance < 0 ? (
											<Badge bg="success">You are owed €{Math.abs(group.balance).toFixed(2)}</Badge>
										) : group.balance > 0 ? (
											<Badge bg="danger">You owe €{Math.abs(group.balance).toFixed(2)}</Badge>
										) : (
											<Badge bg="secondary">Settled</Badge>
										)}
              </Container>
            </Container>
          ))}
        </Col>
      </Row>
      <Row className="d-flex justify-content-center">
          <Button 
            className="w-25 p-3" size="lg"
            onClick={handleAddClick}
          >
            Add new group
          </Button>
      </Row>
    </>
  );
}