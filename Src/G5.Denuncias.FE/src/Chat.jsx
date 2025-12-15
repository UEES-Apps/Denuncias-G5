import { useEffect, useState } from 'react';
import { Link, useParams } from 'react-router-dom';
import { enviarMensaje, obtenerMensajes } from './servicios/buzonService';

function Chat({ usuarioLogueado }) {
  const { id, titulo } = useParams(); 
  const [mensajes, setMensajes] = useState([]);
  const [nuevoTexto, setNuevoTexto] = useState("");

  const recargarMensajes2 = async () => {
    const datos = await obtenerMensajes(id);
    setMensajes(datos);
  };

  useEffect(() => {
    const recargarMensajes = async () => {
      const datos = await obtenerMensajes(id);
      setMensajes(datos);
    };

    recargarMensajes();

    const intervalo = setInterval(recargarMensajes, 3000);
    return () => clearInterval(intervalo);
  }, [id, titulo, usuarioLogueado]);

  const enviar = async (e) => {
    e.preventDefault();
    if (!nuevoTexto.trim()) return;

    await enviarMensaje(id, nuevoTexto, 'usuario', usuarioLogueado.usuario);
    setNuevoTexto("");
    await recargarMensajes2();
  };

  return (
    <div className="card" style={{ maxWidth: '600px', height: '80vh', display: 'flex', flexDirection: 'column' }}>
      <div style={{ borderBottom: '1px solid #444', paddingBottom: '10px', display: 'flex', justifyContent: 'space-between' }}>
        <h3>Chat del Caso #{titulo}</h3>
        <Link to="/buzon">❌ Cerrar</Link>
      </div>


      <div style={{ flex: 1, overflowY: 'auto', padding: '10px', display: 'flex', flexDirection: 'column', gap: '10px' }}>
        {mensajes.length === 0 && <p style={{ textAlign: 'center', color: '#666' }}>No hay mensajes. Escribe a la autoridad.</p>}
        
        {mensajes.map((msg) => (
  <div 
    key={msg.id} 
    className={`mensaje-burbuja ${msg.remitente === 'usuario' ? 'msg-propio' : 'msg-otro'}`}
  >

    <div>{msg.texto}</div>
    <span className="msg-hora">
      {new Date(msg.fecha).toLocaleTimeString([], {hour: '2-digit', minute:'2-digit'})}
    </span>
  </div>
))}
      </div>


      <form onSubmit={enviar} style={{ display: 'flex', gap: '10px', borderTop: '1px solid #444', paddingTop: '10px', alignItems: 'center' }}>
        
        <input
          type="text"
          value={nuevoTexto}
          onChange={(e) => setNuevoTexto(e.target.value)}
          placeholder="Escriba un mensaje..."

          style={{ flex: 1, margin: 0 }}
        />
        
        <button
          type="submit"

          style={{ width: 'auto', margin: 0, padding: '10px 20px' }}
        >
          Enviar
        </button>

      </form>
    </div>
  );
}

export default Chat;