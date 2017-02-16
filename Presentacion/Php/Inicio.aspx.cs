﻿using CrystalDecisions.CrystalReports.Engine;
using Negocio;
using Presentacion.Php.Clases;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Presentacion.Php
{
    public partial class Inicio : System.Web.UI.Page
    {
        ParametrosRpt parametros = new ParametrosRpt();

        protected void Page_Load(object sender, EventArgs e)
        {
            parametros.fecha_desde = Request.QueryString["fecha_desde"];
            parametros.Fecha_hasta = Request.QueryString["fecha_hasta"];
            parametros.id_entidades = Request.QueryString["id_entidades"];

            //ReportDocument crystalReport = new ReportDocument();
            ReportDocument crystalReport = new Php.Reporte.crCuentas();
            var dsCuentas = new Datas.dsCuentas();
            DataTable dt_Reporte = new DataTable();

            //danny
            string columnas = "plan_cuentas.nombre_plan_cuentas,entidades.nombre_entidades,plan_cuentas.nivel_plan_cuentas," +
                              "plan_cuentas.t_plan_cuentas,plan_cuentas.n_plan_cuentas,plan_cuentas.codigo_plan_cuentas";

            string tablas = "public.plan_cuentas, public.entidades";

            string where = "entidades.id_entidades = plan_cuentas.id_entidades";

            string order = "plan_cuentas.codigo_plan_cuentas";

            String where_to = "";

            if (!String.IsNullOrEmpty(parametros.id_entidades)) {

                where_to += " AND entidades.id_entidades = " + parametros.id_entidades;
            }

            where = where + where_to;

            dt_Reporte = AccesoLogica.Select(columnas, tablas, where, order);

            //dsCuentas.Cuentas= dt_Reporte;

            dsCuentas.Tables.Add(dt_Reporte);

            string cadena = Server.MapPath("~/Php/Reporte/crCuentas.rpt");

            string nombre_entidad = dt_Reporte.Rows[0]["nombre_entidades"].ToString();

            crystalReport.SetDataSource(dsCuentas.Tables[1]);

            //crystalReport.Load(cadena);

            //parametros   
            crystalReport.SetParameterValue(@"nombre_entidad",nombre_entidad);         

            //parametros discretos
           /* CrystalDecisions.Shared.ParameterValues RpDatos = new CrystalDecisions.Shared.ParameterValues();
            CrystalDecisions.Shared.ParameterDiscreteValue p_nombre_entidad = new CrystalDecisions.Shared.ParameterDiscreteValue();

            p_nombre_entidad.Value = nombre_entidad;

            RpDatos.Add(p_nombre_entidad);
            crystalReport.DataDefinition.ParameterFields["nombre_entidad"].ApplyCurrentValues(RpDatos);
            RpDatos.Clear();*/
            
            
            CrystalReportViewer1.ReportSource = crystalReport;
        }

        protected void CrystalReportViewer1_Init(object sender, EventArgs e)
        {

        }
    }
}