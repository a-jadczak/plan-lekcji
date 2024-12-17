import React, { useState } from 'react'
import { Button, Form, Modal } from 'react-bootstrap'

const ImportModal = ({ open, onClose }: { open: boolean; onClose: () => void }) => {
    
    const sentFile = async (e: React.FormEvent<HTMLFormElement>) =>
    {
        e.preventDefault();

        const formData = new FormData(e.currentTarget);

        try {
            const response = await fetch('https://localhost:7222/plan-lekcji/nowyPlan', {
                method: "POST",
                body: formData
            });
    
            if (!response.ok) {
                alert(`HTTP error status: ${response.status}`);
                throw new Error(`HTTP error status: ${response.status}`);
            }
    
            const text = await response.text();
            alert(text);
        } catch (error) {
            console.error('Error while sending file:', error);
        }
    }
    
    return (
        <Modal centered show={open} onHide={onClose}>
            <Modal.Header closeButton>
                <Modal.Title>Importuj nowy plan</Modal.Title>
            </Modal.Header>
            <Modal.Body>
                <Form onSubmit={sentFile} encType="multipart/form-data">
                    <Form.Group controlId="formFile" className="mb-3">
                        <Form.Label>Prześlij nowy plan lekcji</Form.Label>
                        <Form.Control type="file" name="file" />
                        <Button type='submit' variant="success" className="mt-3">Prześlij</Button>
                    </Form.Group>
                </Form>
            </Modal.Body>
        </Modal>
    );
};

export default ImportModal