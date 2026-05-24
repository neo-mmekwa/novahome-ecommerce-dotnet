<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="NovaHome_Frontend.login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="hero-wrap hero-bread" style="background-image: url('images/furniture/header.jpg');">
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <p class="breadcrumbs"><span class="mr-2"><a href="index.aspx">Home</a></span> <span>Login</span></p>
                    <h1 class="mb-0 bread">Login</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="ftco-section">
        <div class="container">

            <div class="row justify-content-center">


                <div class="col-xl-10 ftco-animate">

                    <div action="#" class="billing-form">

                        
                        <div class="form-group">
                            <label for="email">Enter Your Email</label>
                            <input type="email" id="email" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="password">Enter Your Password</label>
                            <input type="password" id="password" runat="server" class="form-control">
                        </div>

                        <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
                        <div class="form-group text-center">
                            <!--<input type="submit" value="Login" class="btn btn-primary py-3 px-5">-->
                            <asp:Button ID="btnLogin" runat="server" type="submit" class="btn btn-primary py-3 px-5" Text="Login" OnClick="btnLogin_Click" />
                        </div>
                        <div class="form-group text-center">
                            <p>Don't have an account? <a href="register.aspx">Register</a></p>
                        </div>

                </div>

            </div>

        </div>
    </section>

</asp:Content>
