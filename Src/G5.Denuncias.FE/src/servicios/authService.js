let usuariosDB = [];
export const registrarUsuario = async (usuario, clave) => {
  await new Promise(resolve => setTimeout(resolve, 500));
  const existe = usuariosDB.find(u => u.usuario === usuario);
  if (existe) {
    throw new Error("El usuario ya existe");
  }
  usuariosDB.push({ usuario, clave });
  console.log("Base de datos actual:", usuariosDB); 
  return true;
};
export const loginUsuario = async (usuario, clave) => {
  await new Promise(resolve => setTimeout(resolve, 500));

  const usuarioEncontrado = usuariosDB.find(u => u.usuario === usuario && u.clave === clave);
  
  if (usuarioEncontrado) {
    return { usuario: usuarioEncontrado.usuario, token: 'token-falso-123' };
  } else {
    throw new Error("Credenciales incorrectas");
  }
};