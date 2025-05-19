import Container from 'react-bootstrap/Container';
import { Outlet } from 'react-router';

export default function Layout() {
  return (
    <Container fluid className="bg-light my-5 border-top border-bottom">
      <Container className="py-4 w-75">
        <Outlet />
      </Container>
    </Container>
  );
}