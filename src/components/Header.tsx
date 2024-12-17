import { Button, Container, Image, Nav, Navbar } from 'react-bootstrap'
import Logo from "./../../public/logo/logoLong.svg"
import ClassIcon from './../../public/icons/class.svg'
import Classroom from './../../public/icons/classroom.svg'
import Teacher from './../../public/icons/teacher.svg'
import LessonPlan from '../lib/Types/LessonPlanType'
import EntityModal from './Modals/EntityModal'
import { useEffect, useState } from 'react'
import ImportModal from './Modals/ImportModal'
import ThemeToggle from './ThemeToggle'

const Header = ({ plan, recivedCallback }: { plan: LessonPlan | null; recivedCallback: Function }) => {
    const [openImportModal, setOpenImportModal] = useState<boolean>(false);

    const [open, setOpen] = useState<boolean>(false);
    const [text, setText] = useState<string>("");

    const openEntityModal = (text: string) => {
        setOpen(true);
        setText(text);
    };

    return (
        <>
            <ImportModal open={openImportModal} onClose={() => setOpenImportModal(false)} />

            <EntityModal
                Open={open}
                SetOpen={setOpen}
                Title={text}
                Entities={plan?.Entities}
                CategoryChar={text.charAt(0).toLowerCase()}
                /* @ts-ignore */
                Callback={recivedCallback}
            />

            <Navbar sticky='top' expand="lg" className="bg-body-tertiary shadow-sm">
                <Container fluid>
                    <Navbar.Brand href="#home">
                        <Image src={Logo} style={{ width: "8em" }} />
                    </Navbar.Brand>

                    <Navbar.Toggle aria-controls="basic-navbar-nav" />

                    <Navbar.Collapse id="basic-navbar-nav">
                        <Nav className="ms-auto fs-5">
                            <Nav.Link onClick={() => openEntityModal("Oddziały")}>
                                <Image src={ClassIcon} style={{ width: "20px" }} />
                                <span className="ms-1">Oddziały</span>
                            </Nav.Link>

                            <Nav.Link onClick={() => openEntityModal("Nauczyciele")}>
                                <Image src={Teacher} style={{ width: "20px" }} />
                                <span className="ms-1">Nauczyciele</span>
                            </Nav.Link>

                            <Nav.Link onClick={() => openEntityModal("Sale")}>
                                <Image src={Classroom} style={{ width: "20px" }} />
                                <span className="ms-1">Sale</span>
                            </Nav.Link>

                            <ThemeToggle/>

                            <Button variant='success' className='m-1' onClick={() => setOpenImportModal(true)}>import</Button>
                        </Nav>
                    </Navbar.Collapse>
                </Container>
            </Navbar>
        </>
    );
};


export default Header