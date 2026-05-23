<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="NovaHome_Frontend.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-wrap hero-bread" style="background-image: url('images/bg_6.jpg');">
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <p class="breadcrumbs"><span class="mr-2"><a href="index.aspx">Home</a></span> <span>Register</span></p>
                    <h1 class="mb-0 bread">Register</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="ftco-section contact-section bg-light">
        <div class="container">

            <div class="row block-9">

                <div class="col-md-6 order-md-last d-flex">

                    <div action="#" class="bg-white p-5 contact-form">

                        <div class="form-group">
                            <input type="text" id="fName" runat="server" class="form-control" placeholder="First Name">
                        </div>
                        <div class="form-group">
                            <input type="text" id="lName" runat="server" class="form-control" placeholder="Last Name">
                        </div>
                        <div class="form-group">
                            <input type="text" id="email" runat="server" class="form-control" placeholder="Email">
                        </div>
                        <div class="form-group">
                            <input type="text" id="phone" runat="server" class="form-control" placeholder="Phone Number">
                        </div>
                        <div class="form-group">
                            <input type="password" id="password" runat="server" class="form-control" placeholder="Password">
                        </div>
                        <div class="form-group">
                            <input type="password" id="cPassword" runat="server" class="form-control" placeholder="Confirm Password">
                        </div>
                        <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
                        <div class="form-group text-center">
                            <!--<input type="submit" value="Register" class="btn btn-primary py-3 px-5">-->
                            <asp:Button ID="btnRegister" runat="server" type="submit" class="btn btn-primary py-3 px-5" Text="Register" OnClick="btnRegister_Click"/>
                        </div>
                        <div class="form-group text-center">
                            <p>Already have an account? <a href="login.aspx">Log in</a></p>
                        </div>
                    </div>
                </div>

                <div class="col-md-6 d-flex">
                    <img src="images/image_2.jpg" alt="Description" class="img-fluid bg-white">
                </div>

            </div>

        </div>
    </section>
</asp:Content>
