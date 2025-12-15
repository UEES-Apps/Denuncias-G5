import { useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { crearDenuncia } from './servicios/denunciasService';

function CrearDenuncia({ usuarioLogueado }) {
  const navigate = useNavigate();


  const [formulario, setFormulario] = useState({
    titulo: '',
    descripcion: '',
    fechaEvento: '',
    ciudad: 'Quito',
    esPublica: 'publica',
    tipo: 'Aseo y Ornato'
  });


  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormulario({
      ...formulario,
      [name]: value
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {

      await crearDenuncia({
        ...formulario,
        autor: usuarioLogueado.usuario,
      
      });
      
      alert("¡Denuncia registrada con éxito!");
      navigate('/dashboard');
    } catch (error) {
      alert(`Error al guardar la denuncia: ${error.message}`);
    }
  };

  return (
    <div className="card" style={{ maxWidth: '500px', textAlign: 'left' }}>
      <h2 style={{ textAlign: 'center' }}>Nueva Denuncia 📝</h2>
      
      <form onSubmit={handleSubmit} style={{ display: 'grid', gap: '15px' }}>
        

        <div>
          <label>Título:</label>
          <input
            type="text" name="titulo" required
            value={formulario.titulo} onChange={handleChange}
            placeholder="Ej: Bache en la Av. Amazonas"
          />
        </div>


        <div>
          <label>Descripción:</label>
          <textarea
            name="descripcion" required rows="3"
            value={formulario.descripcion} onChange={handleChange}
            placeholder="Detalle lo sucedido..."
            style={{ width: '100%', padding: '8px' }}
          />
        </div>


        <div style={{ display: 'grid', gridTemplateColumns: '1fr 1fr', gap: '10px' }}>
          <div>
            <label>Fecha del Evento:</label>
            <input
              type="date" name="fechaEvento" required
              value={formulario.fechaEvento} onChange={handleChange}
            />
          </div>
          <div>
            <label>Ciudad:</label>
            <select name="ciudad" value={formulario.ciudad} onChange={handleChange}>
              <option value="Quito">Quito</option>
              <option value="Guayaquil">Guayaquil</option>
              <option value="Cuenca">Cuenca</option>
              <option value="Manta">Manta</option>
              <option value="Ambato">Ambato</option>
            </select>
          </div>
        </div>

        <div>
          <label>Tipo de Denuncia:</label>
          <select name="tipo" value={formulario.tipo} onChange={handleChange} style={{ width: '100%', padding: '8px' }}>
            <option value="Aseo y Ornato">Aseo y Ornato</option>
            <option value="Transito Vial">Tránsito Vial</option>
            <option value="Delito">Delito</option>
          </select>
        </div>


        <div>
          <label>Visibilidad:</label>
          <div style={{ display: 'flex', gap: '20px', marginTop: '5px' }}>
            <label style={{ display: 'flex', alignItems: 'center', gap: '5px' }}>
              <input
                type="radio" name="esPublica" value="publica"
                checked={formulario.esPublica === 'publica'} onChange={handleChange}
              />
              Pública
            </label>
            <label style={{ display: 'flex', alignItems: 'center', gap: '5px' }}>
              <input
                type="radio" name="esPublica" value="privada"
                checked={formulario.esPublica === 'privada'} onChange={handleChange}
              />
              Privada
            </label>
          </div>
        </div>

        <button type="submit" style={{ marginTop: '10px' }}>Guardar Denuncia</button>
      </form>
      
      <Link to="/dashboard" style={{ display: 'block', textAlign: 'center', marginTop: '15px' }}>
        Cancelar
      </Link>
    </div>
  );
}

export default CrearDenuncia;