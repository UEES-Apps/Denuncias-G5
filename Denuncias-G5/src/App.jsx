import { useState } from 'react';
import { BrowserRouter, Routes, Route, Navigate,Link } from 'react-router-dom';
import Login from './Login';
import Registro from './Registro';
import Dashboard from './Dashboard';
import Buzon from './Buzon';
import Chat from './Chat';
import './App.css';
import CrearDenuncia from './CrearDenuncia';
import DenunciasPublicas from './DenunciasPublicas';
function App() {
  const [usuarioLogueado, setUsuarioLogueado] = useState(null);

  const manejarLogout = () => {
    setUsuarioLogueado(null);
  };

  return (
    <BrowserRouter>
    <nav style={{
      display: 'flex',
      justifyContent: 'space-between',
      alignItems: 'center',
      padding: '1rem 2rem',
      backgroundColor: '#202020',
      borderBottom: '1px solid #333',
      marginBottom: '2rem',
      position: 'sticky', 
      top: 0,
      zIndex: 100
    }}>
      <div style={{ fontWeight: 'bold', fontSize: '1.2rem', color: 'white' }}>
        🇪🇨 DenunciasApp
      </div>
      
      <div>

        {!usuarioLogueado ? (
          <span style={{color: '#666'}}>Bienvenido</span>
        ) : (
          <div style={{ display: 'flex', gap: '15px' }}>
             <Link to="/dashboard" style={{ textDecoration: 'none', color: '#ccc' }}>Inicio</Link>
             <Link to="/buzon" style={{ textDecoration: 'none', color: '#ccc' }}>Buzón</Link>
          </div>
        )}
      </div>
    </nav>
      <Routes>

        <Route path="/" element={
           !usuarioLogueado ? <Login onLogin={setUsuarioLogueado} /> : <Navigate to="/dashboard" />
        } />


        <Route path="/registro" element={<Registro />} />


        <Route path="/dashboard" element={
           usuarioLogueado ? (
             <Dashboard usuario={usuarioLogueado} onLogout={manejarLogout} />
           ) : (
             <Navigate to="/" /> 
           )
        } />
        <Route path="/crear-denuncia" element={
           usuarioLogueado ? (
             <CrearDenuncia usuarioLogueado={usuarioLogueado} />
           ) : (
             <Navigate to="/" />
           )
        } />
        <Route path="/buzon" element={
           usuarioLogueado ? <Buzon usuarioLogueado={usuarioLogueado} /> : <Navigate to="/" />
        } />

 
        <Route path="/chat/:id" element={
           usuarioLogueado ? <Chat /> : <Navigate to="/" />
        } />
        <Route path="/publicas" element={
           usuarioLogueado ? <DenunciasPublicas /> : <Navigate to="/" />
        } />



      </Routes>
    </BrowserRouter>
  );
}
export default App;