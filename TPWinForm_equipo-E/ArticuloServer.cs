﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace CatalogoDeArticulos
{
    internal class ArticuloServer
    {
        public List<Articulo> listar()
        {

            List<Articulo> Lista = new List<Articulo>();
            SqlConnection Conex = new SqlConnection();
            SqlCommand Comando = new SqlCommand();
            SqlDataReader Lector;

            try
            {
                Conex.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true";
                Comando.CommandType = System.Data.CommandType.Text;
                Comando.CommandText = "SELECT A.Id, I.Id AS ImagenId, Codigo, Nombre, Descripcion, IdMarca, IdCategoria, CAST(Precio AS DECIMAL(18, 2)) AS Precio, ImagenUrl FROM ARTICULOS A INNER JOIN IMAGENES I ON A.Id = I.IdArticulo";
                Comando.Connection = Conex;
                

                Conex.Open();
                Lector =Comando.ExecuteReader();

                while (Lector.Read())
                {
                    Articulo aux = new Articulo();
                    
                    aux.ID = (int)Lector["Id"];
                    aux.Codigo = (string)Lector["Codigo"];
                    aux.Nombre = (string)Lector["Nombre"];
                    aux.Descripcion = (string)Lector["Descripcion"];
                    aux.IdMarca = (int)Lector["IdMarca"];
                    aux.IdCategoria = (int)Lector["IdCategoria"];
                    aux.Precio = (decimal)Lector["Precio"];

                    Imagen imagen = new Imagen();
                    imagen.ID= (int)Lector["ImagenId"];
                    imagen.ImagenURL = (string)Lector["ImagenUrl"];
                    aux.Imagenes.Add(imagen);


                    Lista.Add(aux);
                }

                Conex.Close();
                return Lista;
            }
            catch (Exception ex)
            {
                
                throw ex; 
            }
        }

        public void Agregar(Articulo nuevo)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("Insert into ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, UrlImagen, Precio)values(" + nuevo.Codigo + ", '" + nuevo.Nombre + "', '" + nuevo.Descripcion + ", '" + nuevo.IdMarca+ ", '" + nuevo.IdCategoria + ", '" + nuevo.Precio);
                datos.ejecutarAccion();
            }
            catch ( Exception exe)
            {
                throw exe;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }
    }
}
