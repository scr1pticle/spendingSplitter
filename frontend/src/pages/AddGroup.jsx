import Breadcrumb from 'react-bootstrap/Breadcrumb';
import Row from 'react-bootstrap/Row';
import Col from 'react-bootstrap/Col';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import { useNavigate } from "react-router";

export default function AddGroup() {
  const navigate = useNavigate();
  
  function handleSubmit(e) {
    e.preventDefault();
    const formData = new FormData(e.target);
    const groupName = formData.get('name');
    fetch('/api/groups', {
        method:"POST",
        headers: {
            'Content-Type': 'application/json'
        },
        body:JSON.stringify({name: groupName})
    })
    .then(() => navigate("/home"))
    .catch((e)=>{
        console.log(`API error: ${e}`);
    });
  }
  
  function handleCancel() {
    navigate("/home");
  }
  
  return (
    <>
      <Row className="mb-3">
        <Col>
          <Breadcrumb>
            <Breadcrumb.Item href="/home">Home</Breadcrumb.Item>
            <Breadcrumb.Item active>Add Group</Breadcrumb.Item>
          </Breadcrumb>
        </Col>
      </Row>
      <Row>
        <Col className="bg-white p-4 rounded-4 d-flex justify-content-center">
          <Form onSubmit={handleSubmit} className="m-5 w-50">
            <Form.Group>
              <Form.Label>Group Name</Form.Label>
              <Form.Control name="name" type="text" placeholder="Enter group name" required />
            </Form.Group>
            
            <div className="d-flex justify-content-between mt-4">
              <Button variant="secondary" onClick={handleCancel}>
                Cancel
              </Button>
              <Button variant="primary" type="submit">
                Create Group
              </Button>
            </div>
          </Form>
        </Col>
      </Row>
    </>
  );
}