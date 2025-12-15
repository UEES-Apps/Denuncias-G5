import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { loginUsuario } from './servicios/authService';

function Login({ onLogin }) {
  const [usuario, setUsuario] = useState('');
  const [clave, setClave] = useState('');
  const [error, setError] = useState('');
  const navigate = useNavigate();

  const manejarLogin = async (e) => {
    e.preventDefault();
    try {
      const { token } = await loginUsuario(usuario, clave);
      const data = { usuario, token };
      onLogin(data);
      navigate('/dashboard');
    } catch (err) {
      setError(err.message);
    }
  };

  return (
    <div className="card">
      <h1>Denuncias Ecuador 🇪🇨</h1>
      <h3>Ingreso al Sistema</h3>
      <form onSubmit={manejarLogin} style={{ display: 'flex', flexDirection: 'column', gap: '10px' }}>
        <input
          type="text"
          placeholder="Usuario"
          onChange={(e) => setUsuario(e.target.value)}
        />
        <input
          type="password"
          placeholder="Clave"
          onChange={(e) => setClave(e.target.value)}
        />
        <button type="submit">Ingresar</button>
      </form>
      {error && <p style={{ color: 'red' }}>{error}</p>}
      <br />
      <p>¿No tienes cuenta? <Link to="/registro">Regístrate aquí (Anónimo)</Link></p>
    </div>
  );
}

export default Login;