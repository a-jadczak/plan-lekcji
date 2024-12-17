import React, { useEffect, useState } from 'react'
import Modal from 'react-bootstrap/Modal';
import { Form } from 'react-bootstrap';
import ModalContent from '../../lib/Types/ModalContentType';


const EntityModal: React.FC<ModalContent> = ({ Open, SetOpen, Title, Entities, CategoryChar, Callback }) => {
    const [name, setName] = useState<string>("");

    const handleClose = () =>     
    {
        SetOpen(false);
        setName("")
    }

    const onSelect = (id: string) =>
    {
        Callback(id)
        handleClose();
    }

    return (
        <Modal centered show={Open} onHide={handleClose}>
            <Modal.Header closeButton>
                <Modal.Title>{Title}</Modal.Title>
                <Form className="ms-4">
                    <Form.Control
                    type="search"
                    placeholder="Wyszukaj"
                    className="me-2"
                    aria-label="Search"
                    onChange={(e) => setName(e.target.value)}
                    />
                </Form>
            </Modal.Header>
            <Modal.Body>
                <div className='modal-list'>
                {
                    Entities?.filter(e => e.Id.charAt(0) == CategoryChar)
                    .filter(e => e.Title.toLowerCase().includes(name.toLowerCase()))
                    .sort((a, b) => a.Title.localeCompare(b.Title))
                    .map(e => 
                        <div key={e.Id} className='modal-list-element mb-2'>
                            <span onClick={() => onSelect(e.Id)}>
                                {e.Title}
                            </span>
                        </div>
                    )
                }
                </div>
            </Modal.Body>
        </Modal>
  )
}

export default EntityModal