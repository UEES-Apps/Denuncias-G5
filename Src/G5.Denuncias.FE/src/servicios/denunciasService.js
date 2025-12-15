let denunciasDB = [];

export const crearDenuncia = async (datosDenuncia) => {
  await new Promise(resolve => setTimeout(resolve, 500));
  
  const nuevaDenuncia = {
    id: Date.now(),
    ...datosDenuncia 
  };
  if (!nuevaDenuncia.fechaCreacion) {
    nuevaDenuncia.fechaCreacion = new Date().toISOString();
  }

  denunciasDB.push(nuevaDenuncia);
  console.log("Denuncia guardada en DB:", nuevaDenuncia);
  return true;
};

export const obtenerDenuncias = async () => {
  await new Promise(resolve => setTimeout(resolve, 500));
  return denunciasDB;
};

export const obtenerDenunciasPublicas = async () => {
  await new Promise(resolve => setTimeout(resolve, 500));
  return denunciasDB.filter(d => d.esPublica === 'publica');
};