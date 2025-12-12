import { useNavigate } from 'react-router-dom';

function Dashboard({ usuario, onLogout }) {
  const navigate = useNavigate();

  return (
    <div style={{ textAlign: 'center' }}>
      <h1>Bienvenido, {usuario?.usuario}</h1>
      <p>Panel de Control - Denuncias Ecuador</p>
      
      <div className="contenedor-tarjetas">

        <div className="tarjeta-usuario" onClick={() => navigate('/crear-denuncia')}>
          <h3>📝 Ingreso de Denuncia</h3>
          <p>Realiza una nueva denuncia anónima.</p>
        </div>


        <div className="tarjeta-usuario" onClick={() => navigate('/buzon')}>
  <h3>mailbox 📬 Buzón Personal</h3>
  <p>Revisa el estado de tus trámites.</p>
</div>


        <div className="tarjeta-usuario" onClick={() => navigate('/publicas')}>
  <h3>📢 Denuncias Públicas</h3>
  <p>Ver denuncias de otros ciudadanos.</p>
</div>
      </div>

      <button 
        style={{ marginTop: '20px', backgroundColor: '#d32f2f' }}
        onClick={() => {
          onLogout();
          navigate('/');
        }}
      >
        Cerrar Sesión
      </button>
    </div>
  );
}

export default Dashboard;