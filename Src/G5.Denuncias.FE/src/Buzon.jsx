import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { obtenerDenuncias } from './servicios/denunciasService';

function Buzon({ usuarioLogueado }) {
  const [misDenuncias, setMisDenuncias] = useState([]);
  const navigate = useNavigate();

  useEffect(() => {
    const cargarDenuncias = async () => {

      const todas = await obtenerDenuncias();

      const lasMias = todas.filter(d => d.autor === usuarioLogueado.usuario);
      setMisDenuncias(lasMias);
    };
    cargarDenuncias();
  }, [usuarioLogueado]);

  return (
    <div className="card">
      <h2>📬 Buzón Personal</h2>
      <p>Seleccione un caso para ver los mensajes de la autoridad:</p>

      {misDenuncias.length === 0 ? (
        <p style={{ color: '#aaa' }}>No tienes denuncias registradas aún.</p>
      ) : (
        <div className="contenedor-tarjetas">
          {misDenuncias.map((denuncia) => (
            <div 
              key={denuncia.id} 
              className="tarjeta-usuario"

              onClick={() => navigate(`/chat/${denuncia.id}/${denuncia.titulo}`)}
              style={{ cursor: 'pointer' }}
            >
              <h3>📂 Caso #{denuncia.titulo}</h3>
              <p><strong>Tema:</strong> {denuncia.titulo}</p>
              <p><small>{new Date(denuncia.fechaCreacion).toLocaleDateString()}</small></p>
              <button style={{ marginTop: '10px' }}>Abrir Chat</button>
            </div>
          ))}
        </div>
      )}
      
      <button onClick={() => navigate('/dashboard')} style={{ marginTop: '20px', background: '#555' }}>
        Volver al Dashboard
      </button>
    </div>
  );
}

export default Buzon;