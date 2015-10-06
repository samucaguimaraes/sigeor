
Partial Class Thumbnail
    Inherits System.Web.UI.Page
#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region

    ' Autor: Fabrizio Gianfratti Manes
    ' Site do Autor: www.gianfratti.com
    ' Data: 05/05/2006
    ' Descri��o: Gerar Thumbnail

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        ' Fazemos apenas uma verifica��o de seguran�a para saber se o nome da imagem foi passado
        If Request.QueryString("NomeImagem") <> "" Then
            GeraThumbnail(Request.QueryString("NomeImagem"), Request.QueryString("Largura"))
        Else
            Response.Write("Voc� deve passar o nome da imagem via QueryString.<br>Ex: Thumbnail.aspx?NomeImagem=Logo.jpej&Largura=100")
        End If

    End Sub

    'Fun��o responsavel por gerar o Thumbnail
    'Est� fun��o recebe 2 par�metros Nome da imagem (Ex. Logo.Jpeg) e a largura que o Thumbnail devera ser criado (Ex.100).
    Sub GeraThumbnail(ByVal NomeImagem As Object, ByVal Largura As Object)

        ' Inicializando os Objetos
        Dim objImage, objThumbnail As System.Drawing.Image
        Dim strServerPath, strFilename As String
        Dim shtWidth, shtHeight As Short
        ' Apontamos a pasta onde se encontra as imagens no formato original, caso esteja na raiz use apenas ("\")
        ' Sera a partir da imagem original que vamos gerar os Thumbnail
        strServerPath = Server.MapPath("Documentos\")
        ' Pegamos o nome da imagem que foi passado via QueryString
        strFilename = strServerPath & NomeImagem
        ' Fazemos  o tratamento de erro, caso a imagem passada na QueryString n�o exista no 
        ' diret�rio original ent�o colocamos uma imagem de erro no lugar da imagem original
        Try
            ' Busca a imagem no diret�rio
            objImage = objImage.FromFile(strFilename)
        Catch
            ' Caso a imagem n�o existe colocamos uma imagem de erro
            objImage = objImage.FromFile(strServerPath & "error.gif")
        End Try
        ' Caso tenhamos passado uma Largura na QueryString a largura ser� respeitada, caso contrario foi definido que a largura do Thumbnail sera 100px
        If Largura = Nothing Then
            shtWidth = objImage.Width
        ElseIf Largura < 1 Then
            shtWidth = 100 'Caso n�o for passado a largura na QueryString ent�o a largura sera de 100px
        Else
            shtWidth = Largura 'Caso voc� tenha definido uma largura na QueryString ent�o sera respeitada
        End If
        ' Fazemos um redimensionamento proporcional da imagem entre largura e altura
        shtHeight = objImage.Height / (objImage.Width / shtWidth)
        ' Neste momento estaremos criando o Thumbnail
        objThumbnail = objImage.GetThumbnailImage(shtWidth, shtHeight, Nothing, System.IntPtr.Zero)
        ' Definimos o ContentType para jpeg
        Response.ContentType = "image/jpeg"
        ' Enviamos o Thumbnail para o Cliente
        objThumbnail.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
        ' Destruimos os Objetos que foram criados
        objImage.Dispose()
        objThumbnail.Dispose()

    End Sub
End Class


