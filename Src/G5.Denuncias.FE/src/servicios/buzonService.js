let mensajesDB = [
    { id: 1, denunciaId: 'ejemplo', remitente: 'autoridad', texto: 'Hola, recibimos su denuncia. Estamos verificando.', fecha: new Date().toISOString() }
  ];
  
  export const obtenerMensajes = async (denunciaId) => {
    await new Promise(resolve => setTimeout(resolve, 300));
    return mensajesDB.filter(m => m.denunciaId == denunciaId);
  };
  
  export const enviarMensaje = async (denunciaId, texto, remitente) => {
    await new Promise(resolve => setTimeout(resolve, 300));
  
    mensajesDB.push({
      id: Date.now(),
      denunciaId: denunciaId,
      remitente: remitente, 
      texto: texto,
      fecha: new Date().toISOString()
    });
  
    if (remitente === 'usuario') {
      setTimeout(() => {
        mensajesDB.push({
          id: Date.now() + 1,
          denunciaId: denunciaId,
          remitente: 'autoridad',
          texto: 'Gracias por la información extra. Un agente revisará esto pronto.',
          fecha: new Date().toISOString()
        });
        console.log("¡La autoridad respondió en background!");
      }, 2000);
    }
  
    return true;
  };