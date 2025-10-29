import React from "react";
import PedidoForm from "./components/PedidoForm";
import { Toaster } from "react-hot-toast";

function App() {
  return (
    <>
      <PedidoForm />
      <Toaster position="top-right" reverseOrder={false} />
    </>
  );
}

export default App;
