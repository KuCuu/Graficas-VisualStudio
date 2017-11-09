﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//#####
using HighCharts_Basic.Modelo;
using HighCharts_Basic.Conexion;
using System.Data;
using System.Configuration;
using MySql.Data.MySqlClient;


namespace HighCharts_Basic.DAO
{
    public class EleccionDAO
    {
        ConexionBD bdConnect = new ConexionBD("myConnection");

        public Eleccionauto mostrarDatos(int carroId)
        {
            Eleccionauto elecc = new Eleccionauto();

            try
            {
                bdConnect.openConnect();
                string SQL = ("SELECT * FROM eleccioncarro WHERE id=@id");
                MySqlCommand cmd = new MySqlCommand(SQL, bdConnect.Connection);
                cmd.Parameters.AddWithValue("@id", carroId);
                MySqlDataReader rd = cmd.ExecuteReader();

                if (rd.Read())
                {
                    elecc.Id = rd.GetInt32(0);
                    elecc.Marca = rd.GetString(1);
                    elecc.Modelo = rd.GetInt32(2);
                    elecc.Velocidad = rd.GetInt32(3);
                    elecc.Estabilidad = rd.GetString(4);
                    elecc.Precio = rd.GetInt32(5);
                }
                bdConnect.closeConnect();
            }
            catch (Exception e)
            {
                throw new Exception("Error al Mostrar", e);
            }

            return elecc;
        }

        public List<Eleccionauto> viewMotos()
        {
            List<Eleccionauto> listBike = new List<Eleccionauto>();
                bdConnect.openConnect();
            //MySqlCommand cmd = new MySqlCommand("SELECT modelo, COUNT(modelo) as velocidad FROM eleccioncarro group by modelo order by COUNT(modelo) desc", bdConnect.Connection);
            MySqlCommand cmd = new MySqlCommand("SELECT marcafavorita, precio FROM eleccioncarro", bdConnect.Connection);
            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {

                while (rd.Read())
                {
                    Eleccionauto elc = new Eleccionauto();
                    elc.Marca = rd["marcafavorita"].ToString();
                    elc.Precio = Convert.ToInt32(rd["precio"].ToString());
                    listBike.Add(elc);

                    
                }
            }
            else
            {
                throw new Exception("No hay registro");
    }
    bdConnect.closeConnect();

            return listBike;
        }

        public List<Eleccionauto> listaMarca()
        {
            List<Eleccionauto> listaMarcas = new List<Eleccionauto>();
            bdConnect.openConnect();
            MySqlCommand cmd = new MySqlCommand("SELECT marcafavorita FROM eleccioncarro", bdConnect.Connection);
            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    Eleccionauto elec = new Eleccionauto();
                    elec.Marca = rd["marcafavorita"].ToString();
                    listaMarcas.Add(elec);
                }
            }
            else
            {
                throw new Exception("No hay datos");
            }
            bdConnect.closeConnect();
            //throw new Exception("lista datos: " + listaMarcas);
            return listaMarcas;

        }
        //LISTAR VELOCIDAD
        public List<Eleccionauto> listaVelocidad()
        {
            List<Eleccionauto> listaVelo = new List<Eleccionauto>();
            bdConnect.openConnect();
            MySqlCommand cmd = new MySqlCommand("SELECT velocidad FROM eleccioncarro", bdConnect.Connection);
            MySqlDataReader rd = cmd.ExecuteReader();
            if (rd.HasRows)
            {
                while (rd.Read())
                {
                    Eleccionauto elec = new Eleccionauto();
                    elec.Velocidad = Convert.ToInt32(rd["velocidad"].ToString());
                    listaVelo.Add(elec);
                }
            }
            else
            {
                throw new Exception("No hay datos");
            }
            bdConnect.closeConnect();
            return listaVelo;

        }


    }
}