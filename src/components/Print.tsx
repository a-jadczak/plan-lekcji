import { Button } from 'react-bootstrap'

const Print = () => {
  return (
    <Button variant='success' className='d-sm-none d-xl-block print-btn' onClick={print}>Drukuj</Button>
  )
}

export default Print