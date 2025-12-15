import { useId } from 'react';
import { handleErrorResponse } from './handleErrorResponse';

const API_BASE_URL = "http://localhost:8080/mensaje/v1";
  
export const obtenerMensajes = async (denunciaId) => {

  const response = await fetch(`${API_BASE_URL}/obtener`, {
    method: "GET",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json",
      "DenunciaId": denunciaId
    }
  });
  
  if (!response.ok) {
    await handleErrorResponse(response);
  }

  return await response.json();
};

export const enviarMensaje = async (denunciaId, texto, remitente, usuario) => {
  const mensaje = {
    id: useId,
    denunciaId: denunciaId,
    usuarioDestino: 'autoridad',
    remitente: remitente,
    texto: texto,
    fecha: new Date().toISOString()
  };

  await enviarMensajeApi(mensaje);

  if (remitente === 'usuario') {
    setTimeout(async () => {
      const mensajeUsr = {
        id: useId,
        denunciaId: denunciaId,
        usuarioDestino: usuario,
        remitente: 'autoridad',
        texto: 'Gracias por la información extra. Un agente revisará esto pronto.',
        fecha: new Date().toISOString()
      };

      await enviarMensajeApi(mensajeUsr);
      console.log("¡La autoridad respondió en background!");
    }, 2000);
  }

  return true;
};

export const enviarMensajeApi = async (mensaje) => {
  const response = await fetch(`${API_BASE_URL}/enviar`, {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(mensaje)
  });
  
  if (!response.ok) {
    await handleErrorResponse(response);
  }

  return true;
};