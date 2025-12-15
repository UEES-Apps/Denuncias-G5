export const handleErrorResponse = async (response) => {
    let errorBody;
    try {
        errorBody = await response.json();
    } catch {
        throw new Error("Error inesperado del servidor");
    }
    
    const message =
            errorBody?.message ||
            errorBody?.errors?.[0]?.message ||
            "Error desconocido";
    
    throw new Error(message);
};