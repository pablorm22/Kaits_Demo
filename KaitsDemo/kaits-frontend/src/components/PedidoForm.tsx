import React, { useState } from "react";
import axios from "axios";
import ClienteSelect from "./ClienteSelect";
import ProductoSelect from "./ProductoSelect";
import toast from "react-hot-toast";

interface Detalle {
  codigoProducto: number;
  descripcion: string;
  cantidad: number;
  precioUnitario: number;
  subtotal: number;
}

const PedidoForm: React.FC = () => {
  const [codigoCliente, setCodigoCliente] = useState<number | null>(null);
  const [productoSeleccionado, setProductoSeleccionado] = useState<{
    codigoProducto: number;
    descripcion: string;
  } | null>(null);
  const [cantidad, setCantidad] = useState<number>(1);
  const [precio, setPrecio] = useState<number>(0);
  const [detalles, setDetalles] = useState<Detalle[]>([]);

  // 🔹 Estados para reiniciar selects después de guardar
  const [resetCliente, setResetCliente] = useState(false);
  const [resetProducto, setResetProducto] = useState(false);

  const handleSelectCliente = (id: number) => {
    setCodigoCliente(id);
    toast.success("Cliente seleccionado correctamente");
  };

  const handleAgregar = () => {
    if (!productoSeleccionado || cantidad <= 0 || precio <= 0) {
      toast.error("Completa los campos del producto correctamente");
      return;
    }

    const subtotal = cantidad * precio;

    setDetalles((prev) => [
      ...prev,
      {
        codigoProducto: productoSeleccionado.codigoProducto,
        descripcion: productoSeleccionado.descripcion,
        cantidad,
        precioUnitario: precio,
        subtotal,
      },
    ]);

    setCantidad(1);
    setPrecio(0);
    toast.success("Producto agregado al pedido");
  };

  const handleEliminar = (index: number) => {
    setDetalles(detalles.filter((_, i) => i !== index));
    toast("Producto eliminado", { icon: "🗑️" });
  };

  const handleGuardarPedido = async () => {
    if (!codigoCliente || detalles.length === 0) {
      toast.error("Selecciona un cliente y al menos un producto");
      return;
    }

    const pedido = {
      idOrden: 0,
      fechaOrden: new Date().toISOString(),
      codigoCliente: codigoCliente.toString(),
      precioTotal: detalles.reduce((acc, d) => acc + d.subtotal, 0),
      detalles: detalles.map((d) => ({
        idDetalle: 0,
        idOrden: 0,
        codigoProducto: d.codigoProducto.toString(),
        descripcionProducto: d.descripcion,
        cantidad: d.cantidad,
        precioUnitario: d.precioUnitario,
        subtotal: d.subtotal,
      })),
    };

    try {
      await axios.post("https://localhost:7216/api/Pedidos", pedido);
      toast.success("✅ Pedido guardado correctamente");

      // 🔹 Reiniciar todo el formulario
      setDetalles([]);
      setCodigoCliente(null);
      setProductoSeleccionado(null);
      setCantidad(1);
      setPrecio(0);

      // 🔹 Reiniciar selects
      setResetCliente(true);
      setResetProducto(true);

      // 🔹 Resetear flags luego de un pequeño retardo
      setTimeout(() => {
        setResetCliente(false);
        setResetProducto(false);
      }, 100);
    } catch (error) {
      console.error("Error al guardar el pedido:", error);
      toast.error("❌ Error al guardar el pedido");
    }
  };

  const total = detalles.reduce((acc, d) => acc + d.subtotal, 0);

  return (
    <div className="max-w-3xl mx-auto bg-white p-8 mt-10 rounded-2xl shadow-lg border border-gray-200">
      <h1 className="text-3xl font-bold text-blue-700 mb-6 flex items-center gap-2">
        🧾 Registrar Pedido
      </h1>

      <div className="flex items-center gap-2 mb-6 text-gray-700">
        <span className="text-xl">📅</span>
        <span>Fecha: {new Date().toISOString().slice(0, 10)}</span>
      </div>

      {/* 🔹 Panel de selección de cliente (100% ancho) */}
      <div className="mb-8 text-gray-800">
        <h2 className="text-lg font-semibold mb-3 flex items-center gap-2">
          👤 Seleccionar Cliente
        </h2>

        <div className="p-4 border border-gray-200 rounded-lg bg-gray-50 w-full">
          <ClienteSelect onSelect={handleSelectCliente} reset={resetCliente} />
        </div>
      </div>

      {/* 🔹 Panel de selección de producto */}
      <div className="mb-6 text-gray-800">
        <h2 className="text-lg font-semibold mb-3 flex items-center gap-2">
          📦 Seleccionar Producto
        </h2>

        <div className="p-4 border border-gray-200 rounded-lg bg-gray-50">
          <div className="grid grid-cols-1 md:grid-cols-5 gap-4 items-end">
            <div className="md:col-span-2">
              <label className="block text-sm font-medium mb-1">Producto:</label>
              <ProductoSelect onSelect={setProductoSeleccionado} reset={resetProducto} />
            </div>

            <div>
              <label className="block text-sm font-medium mb-1">Cantidad:</label>
              <input
                type="number"
                className="border border-gray-300 rounded-md p-2 text-sm w-full"
                value={cantidad}
                onChange={(e) => setCantidad(Number(e.target.value))}
                min={1}
              />
            </div>

            {/* 🔹 Campo Precio + botón Agregar */}
            <div className="md:col-span-2 flex items-end gap-2">
              <div className="flex-1">
                <label className="block text-sm font-medium mb-1">Precio:</label>
                <input
                  type="number"
                  className="border border-gray-300 rounded-md p-2 text-sm w-full"
                  value={precio}
                  onChange={(e) => setPrecio(Number(e.target.value))}
                  min={0}
                />
              </div>

              <button
                type="button"
                onClick={handleAgregar}
                className="bg-green-600 text-white px-4 py-2 rounded-md hover:bg-green-700 transition text-sm mt-auto"
              >
                + Agregar
              </button>
            </div>
          </div>
        </div>
      </div>

      {/* 🔹 Tabla de detalles */}
      {detalles.length > 0 && (
        <table className="w-full border-collapse text-sm mb-4 mt-4">
          <thead className="bg-blue-50 text-gray-700">
            <tr>
              <th className="border p-2 text-left">Producto</th>
              <th className="border p-2 text-center w-24">Cantidad</th>
              <th className="border p-2 text-center w-24">Precio</th>
              <th className="border p-2 text-center w-24">Subtotal</th>
              <th className="border p-2 text-center w-20">Eliminar</th>
            </tr>
          </thead>
          <tbody>
            {detalles.map((d, i) => (
              <tr key={i} className="hover:bg-gray-50">
                <td className="border p-2">{d.descripcion}</td>
                <td className="border p-2 text-center">{d.cantidad}</td>
                <td className="border p-2 text-center">
                  {d.precioUnitario.toFixed(2)}
                </td>
                <td className="border p-2 text-center">
                  {d.subtotal.toFixed(2)}
                </td>
                <td className="border p-2 text-center">
                  <button
                    onClick={() => handleEliminar(i)}
                    className="bg-red-500 hover:bg-red-600 text-white px-2 py-1 rounded-md text-xs"
                  >
                    ❌
                  </button>
                </td>
              </tr>
            ))}
          </tbody>
        </table>
      )}

      {/* 🔹 Total */}
      <div className="text-right text-lg font-semibold text-gray-700 mb-4">
        💰 Total:{" "}
        <span className="text-blue-700 font-bold">
          S/. {total.toFixed(2)}
        </span>
      </div>

      {/* 🔹 Botón Guardar */}
      <button
        type="button"
        onClick={handleGuardarPedido}
        className="w-full bg-blue-600 text-white py-3 rounded-md hover:bg-blue-700 transition text-base font-medium"
      >
        Guardar pedido
      </button>
    </div>
  );
};

export default PedidoForm;
