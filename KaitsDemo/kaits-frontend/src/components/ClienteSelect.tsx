import React, { useEffect, useState } from "react";
import axios from "axios";

interface Cliente {
  codigoCliente: number;
  nombreApellido: string;
}

interface ClienteSelectProps {
  onSelect: (codigoCliente: number) => void;
  reset?: boolean;
}

const ClienteSelect: React.FC<ClienteSelectProps> = ({ onSelect, reset }) => {
  const [clientes, setClientes] = useState<Cliente[]>([]);
  const [selected, setSelected] = useState<string>("");

  useEffect(() => {
    const fetchClientes = async () => {
      try {
        const response = await axios.get("https://localhost:7216/api/Clientes");
        setClientes(response.data);
      } catch (error) {
        console.error("Error al cargar los clientes:", error);
      }
    };
    fetchClientes();
  }, []);

  // 🔹 Reinicia el select cuando el padre cambia reset
  useEffect(() => {
    if (reset) setSelected("");
  }, [reset]);

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    setSelected(value);
    if (value) onSelect(Number(value));
  };

  return (
    <select
      value={selected}
      onChange={handleChange}
      className="w-full border border-gray-300 rounded-md p-2 text-sm focus:ring-2 focus:ring-blue-500"
    >
      <option value="">-- Seleccione un cliente --</option>
      {clientes.map((cliente) => (
        <option key={cliente.codigoCliente} value={cliente.codigoCliente}>
          {cliente.nombreApellido}
        </option>
      ))}
    </select>
  );
};

export default ClienteSelect;
