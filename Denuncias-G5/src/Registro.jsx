import { useState } from 'react';
import { useNavigate, Link } from 'react-router-dom';
import { registrarUsuario } from './servicios/authService';

function Registro() {
  const [usuario, setUsuario] = useState('');
  const [clave, setClave] = useState('');
  const [mensaje, setMensaje] = useState('');
  const navigate = useNavigate(); 

  const manejarRegistro = async (e) => {
    e.preventDefault(); 
    try {
      await registrarUsuario(usuario, clave);
      alert("¡Registro exitoso! Ahora inicia sesión.");
      navigate('/'); 
    } catch (error) {
      setMensaje(error.message);
    }
  };

  return (
    <div className="card">
      <h2>Registro Anónimo 🕵️</h2>
      <form onSubmit={manejarRegistro} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
        <input 
          type="text" 
          placeholder="Nombre de Usuario" 
          value={usuario}
          onChange={(e) => setUsuario(e.target.value)}
          required
        />
        <input 
          type="password" 
          placeholder="Clave de Ingreso" 
          value={clave}
          onChange={(e) => setClave(e.target.value)}
          required
        />
        <button type="submit">Registrarse</button>
      </form>
      {mensaje && <p style={{ color: 'red' }}>{mensaje}</p>}
      <p>¿Ya tienes cuenta? <Link to="/">Inicia Sesión</Link></p>
    </div>
  );
}

export default Registro;