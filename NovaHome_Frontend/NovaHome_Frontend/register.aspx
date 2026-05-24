<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="register.aspx.cs" Inherits="NovaHome_Frontend.register" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-wrap hero-bread" style="background-image: url('images/furniture/header.jpg');">
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <p class="breadcrumbs"><span class="mr-2"><a href="index.aspx">Home</a></span> <span>Register</span></p>
                    <h1 class="mb-0 bread">Register</h1>
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
                            <label for="fName">Enter Your First Name</label>
                            <input type="text" id="fName" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="lName">Enter Your Last Name</label>
                            <input type="text" id="lName" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="email">Enter Your Email</label>
                            <input type="text" id="email" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="phoneNumber">Enter Your Phone Number</label>
                            <input type="text" id="phone" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="password">Enter Your Password</label>
                            <input type="password" id="password" runat="server" class="form-control">
                        </div>
                        <div class="form-group">
                            <label for="confirmPassword">Confirm Your Password</label>
                            <input type="password" id="cPassword" runat="server" class="form-control">
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

            </div>

        </div>
    </section>
</asp:Content>
