import { useEffect, useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { obtenerDenunciasPublicas } from './servicios/denunciasService';

function DenunciasPublicas() {
  const [denuncias, setDenuncias] = useState([]);
  const [filtroTiempo, setFiltroTiempo] = useState('semana');
  const navigate = useNavigate();


  useEffect(() => {
    const cargarDatos = async () => {
      const data = await obtenerDenunciasPublicas();
      setDenuncias(data);
    };

    cargarDatos();
  }, []);



  const denunciasFiltradas = denuncias.filter(d => {
    const fechaDenuncia = new Date(d.fechaCreacion);
    const ahora = new Date();
    
 
    const diferenciaMs = ahora - fechaDenuncia;
    const horasDiferencia = diferenciaMs / (1000 * 60 * 60);

    if (filtroTiempo === 'dia') {
      return horasDiferencia <= 24; 
    } else if (filtroTiempo === 'semana') {
      return horasDiferencia <= (24 * 7); 
    }
    return true;
  });

  return (
    <div className="card" style={{ maxWidth: '800px' }}>
      <div style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', marginBottom: '20px' }}>
        <h2>📢 Muro Público</h2>
        <button onClick={() => navigate('/dashboard')} style={{ background: '#555', padding: '5px 10px' }}>
          Volver
        </button>
      </div>

      {/* BOTONES DE FILTRO */}
      <div style={{ display: 'flex', gap: '10px', marginBottom: '20px', justifyContent: 'center' }}>
        <button 
          onClick={() => setFiltroTiempo('dia')}
          style={{ backgroundColor: filtroTiempo === 'dia' ? '#646cff' : '#333' }}
        >
          Últimas 24 Horas
        </button>
        <button 
          onClick={() => setFiltroTiempo('semana')}
          style={{ backgroundColor: filtroTiempo === 'semana' ? '#646cff' : '#333' }}
        >
          Última Semana
        </button>
      </div>

      {/* LISTADO */}
      <div className="contenedor-tarjetas">
        {denunciasFiltradas.length === 0 ? (
          <p style={{ gridColumn: '1/-1', textAlign: 'center', color: '#aaa' }}>
            No hay denuncias recientes en este periodo.
          </p>
        ) : (
          denunciasFiltradas.map((d) => (
            <div key={d.id} className="tarjeta-usuario" style={{ textAlign: 'left', borderLeft: '4px solid #ff9800' }}>
              <div style={{ display: 'flex', justifyContent: 'space-between' }}>
                <span className="etiqueta" style={{ background: '#444', fontSize: '0.7rem' }}>{d.ciudad}</span>
                <small style={{ color: '#aaa' }}>
                  {new Date(d.fechaCreacion).toLocaleDateString()}
                </small>
              </div>
              
              <h3 style={{ margin: '10px 0' }}>{d.titulo}</h3>
              <p style={{ color: '#ddd' }}>{d.descripcion}</p>
              
              <div style={{ marginTop: '10px', fontSize: '0.8rem', color: '#888' }}>
                Categoría: <strong>{d.tipo}</strong>
              </div>
            </div>
          ))
        )}
      </div>
    </div>
  );
}

export default DenunciasPublicas;