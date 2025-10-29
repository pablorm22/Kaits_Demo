import React, { useEffect, useState } from "react";
import axios from "axios";

interface Producto {
  codigoProducto: number;
  descripcion: string;
}

interface ProductoSelectProps {
  onSelect: (producto: { codigoProducto: number; descripcion: string }) => void;
  reset?: boolean;
}

const ProductoSelect: React.FC<ProductoSelectProps> = ({ onSelect, reset }) => {
  const [productos, setProductos] = useState<Producto[]>([]);
  const [selected, setSelected] = useState<string>("");

  useEffect(() => {
    const fetchProductos = async () => {
      try {
        const response = await axios.get("https://localhost:7216/api/Productos");
        setProductos(response.data);
      } catch (error) {
        console.error("Error al cargar los productos:", error);
      }
    };
    fetchProductos();
  }, []);

  // 🔹 Reinicia el select cuando el padre cambia reset
  useEffect(() => {
    if (reset) setSelected("");
  }, [reset]);

  const handleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const value = e.target.value;
    setSelected(value);

    const producto = productos.find(
      (p) => p.codigoProducto.toString() === value
    );
    if (producto) {
      onSelect({
        codigoProducto: producto.codigoProducto,
        descripcion: producto.descripcion,
      });
    }
  };

  return (
    <select
      value={selected}
      onChange={handleChange}
      className="w-full border border-gray-300 rounded-md p-2 text-sm focus:ring-2 focus:ring-blue-500"
    >
      <option value="">-- Seleccione un producto --</option>
      {productos.map((producto) => (
        <option key={producto.codigoProducto} value={producto.codigoProducto}>
          {producto.descripcion}
        </option>
      ))}
    </select>
  );
};

export default ProductoSelect;
