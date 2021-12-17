<%@ Page Title="User Registration" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="UserRegistration.aspx.cs" Inherits="UserRegistration.UserRegistration" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <table>
        <tr>
            <td>First Name :
            </td>
            <td>
                <asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Last Name :
            </td>
            <td>
                <asp:TextBox ID="txtLastName" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Email :
            </td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Contact No:
            </td>
            <td>
                <asp:TextBox ID="txtContactNo" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>Gender :
            </td>
            <td>
                <asp:RadioButton ID="radMale" GroupName="gender" runat="server" /><asp:RadioButton GroupName="gender" ID="radFemale" runat="server" />
            </td>
        </tr>
        <tr>
            <td>Date of Birth :
            </td>
            <td>
                <asp:Calendar ID="calDateofBirth" runat="server"></asp:Calendar>
            </td>
        </tr>
        <tr>
            <td>City :
            </td>
            <td>
                <asp:DropDownList ID="cmbCity" runat="server"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Photo :
            </td>
            <td>
                <asp:FileUpload ID="FileUpload1" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" /><asp:Button ID="btnReset" runat="server" Text="Reset" OnClick="btnReset_Click" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="false" DataKeyNames="UserRegID"
                    OnPageIndexChanging="GridView1_PageIndexChanging" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                    OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                    <Columns>
                        <asp:BoundField DataField="UserRegID" HeaderText="User ID" />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name" />
                        <asp:BoundField DataField="LastName" HeaderText="Last Name" />
                        <asp:BoundField DataField="ContactNo" HeaderText="CounContact No" />
                        <asp:BoundField DataField="EmailID" HeaderText="EmailID" />
                        <asp:CommandField ShowEditButton="true" />
                        <asp:CommandField ShowDeleteButton="true" />
                    </Columns>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
