import { useEffect, useState } from 'react'
import Plan from './components/Plan.tsx'
import 'bootstrap/dist/css/bootstrap.css';
import Header from './components/Header.tsx';
import LessonPlan from './lib/Types/LessonPlanType.ts';

function App() 
{
  const [plan, setPlan] = useState<LessonPlan | null>(null);

  //@ts-ignore
  const [setEntityCallback, setSetEntityCallback] = useState<(value: string) => void | null>(null);

  const handleReceiveCallback = (callback: (data: string) => void) => {
    setSetEntityCallback(() => callback); // Przechowuje callback z Planu
  };
  
  useEffect(() => 
  {
    fetch("./../../data/data.json")
      .then(response => response.json())  // Parsowanie odpowiedzi do obiektu JSON
      .then(data => setPlan(data))        // Ustawienie danych w stanie
      .catch(error => console.error('Error loading the JSON file:', error));
  }, []);

  return (
    <>
      <Header plan={plan} recivedCallback={setEntityCallback}/>
      <Plan plan={plan} sendCallback={handleReceiveCallback}/>
    </>
  )
}

export default App
