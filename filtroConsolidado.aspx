﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="filtroConsolidado.aspx.cs" Inherits="despacho.filtroConsolidado" %>
<asp:Content ID="Content1" ContentPlaceHolderID="scripts" runat="server">
    <script>
        
    </script>
    <style>
        .panel {
            position: relative;
           padding-top: 56.25%;
            height: 0;
            overflow: auto;
        }
        .panel iframe{
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            border: 0;
        }
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <!-- Top Bar Starts --><asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <div class="top-bar clearfix">
        <div class="row gutter">
            <div class="col-lg-6 col-md-6 col-sm-6 col-xs-12">
                <div class="page-title">
                    <h3>Reporte Acumulado</h3>
                    <p>/ <a href="filtroConsolidado.aspx">Reporte Acumulado</a></p>
                </div>
            </div>
           
        </div>
    </div>
    <!-- Top Bar Ends -->

    <!-- Row Starts -->
    <div class="row">
        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
            <div class="panel">
                <iframe id="frame" class="mi-frame" runat="server" src="filtroConsolidado.asp" allowfullscreen=""></iframe>
            </div>
        </div>
    </div>
     <!-- Row Starts -->

</asp:Content>