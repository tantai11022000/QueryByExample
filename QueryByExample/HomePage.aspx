<%@ Page Title="" Language="C#" MasterPageFile="~/Main.master" AutoEventWireup="true" CodeBehind="HomePage.aspx.cs" Inherits="QueryByExample.HomePage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style>
.chkChoice input 
{ 
    margin-top: 20px
}
.chkChoice td 
{ 
    padding-top: 20px; 
}
    </style>
        <div id="header">
                <asp:Label ID="LabelTitle" runat="server" Text="Nhập tiêu đề báo cáo: "></asp:Label>
                <asp:TextBox ID="TextBoxNhapTieuDe" runat="server"></asp:TextBox>    
        </div>
        <div id="main">
            <div id="table-content">
                <asp:Panel ID="PanelChonBang" runat="server" BackColor="White" ForeColor="Red">
                    <asp:Label ID="LabelChonBang" runat="server" Text="Chọn bảng: "></asp:Label>
                    
                    <asp:CheckBoxList ID="CheckBoxListTable" runat="server" Height="20px" OnSelectedIndexChanged="CheckBoxListTable_SelectedIndexChanged" Width="500px">
                    </asp:CheckBoxList>
                    
                    
                </asp:Panel>
            </div>
        
            <div id="column-content">
                <asp:Panel ID="PanelChonCot" runat="server" BackColor="Black" ForeColor="White">

                    <br />
                    <asp:Label ID="LabelChonCot" runat="server" Text="Chọn cột: "></asp:Label>
                    <br />
                 
                    <asp:CheckBoxList ID="CheckBoxListColumn" runat="server" Height="20px" CellSpacing="100" CssClass="chkChoice" OnSelectedIndexChanged="CheckBoxListColumn_SelectedIndexChanged" Width="100%" CellPadding="100">              
                    </asp:CheckBoxList>

                  
                   
                    <br />
                    <asp:Button ID="ButtonClearColumn" runat="server" OnClick="ButtonClearColumn_Click" Text="Bỏ chọn hết các cột" />
                    <br />
                    

                </asp:Panel>
            </div>

            <div id="query-content">
                <asp:Panel ID="PanelGridViewColumn" runat="server" BackColor="Orange" ForeColor="Black">                
                
                    <asp:GridView ID="GridView1" runat="server" BackColor="White"  BorderColor="#CCCCCC" Width="100%" BorderWidth="1px" CellPadding="3" Height="16px" >
                       <Columns>
                           
                  
                       
                
                           <asp:TemplateField HeaderText="Hàm" >
                               <ItemTemplate>
                                   <asp:DropDownList ID="DropDownList1" Width="100%" runat="server">
                                        <asp:ListItem Text="Non_Selected" Value=""></asp:ListItem>
                                        
                                        <asp:ListItem Text="COUNT" Value="COUNT"></asp:ListItem>
                                        <asp:ListItem Text="MIN" Value="MIN"></asp:ListItem>                                     
                                        <asp:ListItem Text="MAX" Value="MAX"></asp:ListItem>
                                       <asp:ListItem Text="SUM" Value="SUM"></asp:ListItem>
                                       <asp:ListItem Text="AVG" Value="AVG"></asp:ListItem>
                                   </asp:DropDownList>
                                </ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Sắp xếp" >
                               <ItemTemplate>
                                   <asp:DropDownList ID="DropDownList2" Width="100%" runat="server">
                                        <asp:ListItem Text="Non_Selected" Value=""></asp:ListItem>
                                        <asp:ListItem Text="SORT ASC" Value="ASC"></asp:ListItem>
                                        <asp:ListItem Text="SORT DESC" Value="DESC"></asp:ListItem>
                                        
 
                                   </asp:DropDownList>
                                </ItemTemplate>
                           </asp:TemplateField>
                           
              
                   
                           <asp:TemplateField HeaderText="Điều Kiện">
                               <ItemTemplate>
                                   <asp:TextBox ID="TextBoxDieuKien" Width="98%" runat="server"></asp:TextBox>
                               </ItemTemplate>
                           </asp:TemplateField>
                       </Columns>
                       <FooterStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
                       <HeaderStyle BackColor="#006699" Font-Bold="true" ForeColor="White"/>
                       <PagerStyle BackColor="White" ForeColor="#000066" HorizontalAlign="Left"/>
                       <RowStyle ForeColor="#000066"/>
                       <SelectedRowStyle BackColor="#006699" ForeColor="White" Font-Bold="true"/>
                       <SortedAscendingCellStyle BackColor="#000066"/>
                       <SortedAscendingHeaderStyle BackColor="#000066"/>
                       <SortedAscendingCellStyle BackColor="#007DBB"/>
                       <SortedDescendingCellStyle BackColor="#CAC9C9"/>
                       <SortedDescendingHeaderStyle BackColor="#00547E"/>
                    </asp:GridView>


                    
                   
                </asp:Panel>
            </div>
            <div id="create-query-content">
                <br />
                    <asp:Label ID="lbltxt" runat="server"></asp:Label>
                   <br />
                    <asp:Button ID="ButtonQuery" runat="server" OnClick="ButtonQuery_Click" Text="Tạo câu truy vấn" />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" Height="70px" TextMode="MultiLine" Width="1398px" Wrap="False"></asp:TextBox>
                    <br />
                    <br />
            </div>
            <div id="report-content">        
                <br />   
                <br />
                 <asp:Button ID="btnReport" runat="server" OnClick="ButtonReport_Click" Text="Tạo báo cáo" />
                <br />
                <br />
            </div>
        </div>
</asp:Content>

