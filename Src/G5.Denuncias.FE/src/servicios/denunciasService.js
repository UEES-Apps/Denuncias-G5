import { useId } from 'react';
import { handleErrorResponse } from './handleErrorResponse';

const API_BASE_URL = "http://localhost:8080/denuncia/v1";

export const crearDenuncia = async (datosDenuncia) => {
  
  console.log("crearDenuncia - datosDenuncia:", datosDenuncia );
  const nuevaDenuncia = {
    id: useId,
    ...datosDenuncia
  };
  if (!nuevaDenuncia.fechaCreacion) {
    nuevaDenuncia.fechaCreacion = new Date().toISOString();
  }

  console.log(`crearDenuncia - ${API_BASE_URL}/crear - nuevaDenuncia:`, nuevaDenuncia );

  const response = await fetch(`${API_BASE_URL}/crear`, {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify(nuevaDenuncia)
  });

  console.log(`crearDenuncia - ${API_BASE_URL}/crear - response:`, response );
  
  if (!response.ok) {
    await handleErrorResponse(response);
  }

  console.log("Denuncia guardada en DB:", nuevaDenuncia);
  return true;
};

export const obtenerDenuncias = async () => {

  const response = await fetch(`${API_BASE_URL}/obtener`, {
    method: "GET",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    }
  });
  
  if (!response.ok) {
    await handleErrorResponse(response);
  }
  
  return await response.json();
};

export const obtenerDenunciasPublicas = async () => {

  const response = await fetch(`${API_BASE_URL}/denunciaspublicas/obtener`, {
    method: "GET",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    }
  });
  
  if (!response.ok) {
    await handleErrorResponse(response);
  }
  
  return await response.json();
};
