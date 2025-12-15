import { handleErrorResponse } from './handleErrorResponse';

const API_BASE_URL = "http://localhost:8080/usuario/v1";

export const registrarUsuario = async (usuario, clave) => {
  const response = await fetch(`${API_BASE_URL}/registrar`, {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      nombreUsuario: usuario,
      claveHash: clave
    })
  });

  if (!response.ok) {
    await handleErrorResponse(response);
  }

  // 200 OK
  return await response.json();
};

export const loginUsuario = async (usuario, clave) => {
  const response = await fetch(`${API_BASE_URL}/autenticar`, {
    method: "POST",
    headers: {
      "Accept": "application/json",
      "Content-Type": "application/json"
    },
    body: JSON.stringify({
      nombreUsuario: usuario,
      claveHash: clave
    })
  });

  if (!response.ok) {
    await handleErrorResponse(response);
  }

  // 200 OK
  return await response.json();
};