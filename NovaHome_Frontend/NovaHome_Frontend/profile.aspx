<%@ Page Title="" Language="C#" MasterPageFile="~/Master.Master" AutoEventWireup="true" CodeBehind="profile.aspx.cs" Inherits="NovaHome_Frontend.profile" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="hero-wrap hero-bread" style="background-image: url('images/furniture/header.jpg');">
        <div class="container">
            <div class="row no-gutters slider-text align-items-center justify-content-center">
                <div class="col-md-9 ftco-animate text-center">
                    <p class="breadcrumbs"><span class="mr-2"><a href="index.aspx">Home</a></span> <span>Profile</span></p>
                    <h1 class="mb-0 bread">My Profile</h1>
                </div>
            </div>
        </div>
    </div>

    <section class="ftco-section">
        <div class="container">
            <div class="row justify-content-center">
                <div class="col-xl-10 ftco-animate">

                    <div action="#" class="billing-form">

                        <!--EDIT PASSWORD-->
                        <h3 class="mb-4 billing-heading">Profile Details</h3>

                        <div class="row align-items-end">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="firstname">First Name</label>
                                    <asp:TextBox ID="txtFirstName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="lastname">Last Name</label>
                                    <asp:TextBox ID="txtLastName" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="firstname">Email Address</label>
                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="lastname">Phone Number</label>
                                    <asp:TextBox ID="txtPhone" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group text-center ">
                                <asp:Button ID="btnEditProfile" runat="server" type="submit" CssClass="btn btn-primary py-3 px-5" Text="Edit Profile" OnClick="btnEditProfile_Click" />
                                 <asp:Label ID="lblResponseProfile" runat="server" Text=""></asp:Label>
                            </div>
                        </div>

                        <br />
                        <br />
                        <br />

                        <!--RESET PASSWORD-->

                        <h3 class="mb-4 billing-heading">Reset Password</h3>

                        <div class="row align-items-end" id="divResetPassword">

                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="password">New Password</label>
                                    <asp:TextBox ID="txtPassword" runat="server" type="password" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-6">
                                <div class="form-group">
                                    <label for="confirmPassword">Confirm Password</label>
                                    <asp:TextBox ID="txtConfirmPassword" runat="server" type="password" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>

                            
                            <div class="form-group text-center">
                                <asp:Button ID="btnResetPassword" runat="server" type="submit" CssClass="btn btn-primary py-3 px-5" Text="Reset Password" OnClick="btnResetPassword_Click" />
                                <asp:Label ID="lblResponsePass" runat="server" Text=""></asp:Label>
                            </div>

                        </div>
                        <br />
                        <br />
                        <br />
                        <!--DELETE ACCOUNT-->

                        <h3 class="mb-4 billing-heading">Delete Account</h3>
                        <p>If you wish to delete your account please confirm your password. You will be unable to log in again and will need to create a new account. </p>


                        <div class="row align-items-end">
                            <!--
                            <div class="col-md-6"
                                <div class="form-group">
                                    <label for="firstname">Reason for deleting account</label>
                                    <input type="text" class="form-control" placeholder="">
                                </div>
                            </div>-->
                            
                            <div class="col-xl-10">
                                <div class="form-group">
                                    <label for="lastname">Confirm Password</label>
                                    <asp:TextBox ID="txtDeletePass" runat="server" type="password" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>


                            <asp:Label ID="lblDeleteacc" runat="server" Text=""></asp:Label>
                            <div class="form-group text-center">
                                <asp:Button ID="btnDeleteAccount" runat="server" type="submit" CssClass="btn btn-primary py-3 px-5" Text="Delete Profile" OnClick="btnDeleteAccount_Click" />
                                <asp:Label ID="lblResponseDelete" runat="server" Text=""></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
