FORM 1
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class FBiblioteka

    Dim conBG As New MySql.Data.MySqlClient.MySqlConnection("server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekaglowna")
    Dim conBW As New MySql.Data.MySqlClient.MySqlConnection("server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekawojewodzka")


    Private Sub polacz_z_BG()

        Dim myConnectionString As String
        myConnectionString = "server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekaglowna"
        TxtConnection.Clear()
        conBW.Close()
        Try
            conBG.ConnectionString = myConnectionString
            conBG.Open()
            TabKatalog.SelectedIndex = 1
            FBibliotekaPracownik.Close()
        Catch ex As Exception
            MessageBox.Show("Błąd połączenia z Bazą Danych", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub polacz_z_BW()

        Dim myConnectionString As String
        myConnectionString = "server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekawojewodzka"
        TxtConnection.Clear()
        conBG.Close()

        Try
            conBW.ConnectionString = myConnectionString
            conBW.Open()
            TabKatalog.SelectedIndex = 1
            FBibliotekaPracownik.Close()
        Catch ex As Exception
            MessageBox.Show("Błąd połączenia z Bazą Danych", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub czysc_wpis()
        CBGatunek.Text = " "
        CBAutor.Text = " "
        CBTytul.Text = " "
        CBNrPoz.Text = " "
        TxtWydawnictwo.Text = ""
        PB_Ksiazka.Image = Nothing

    End Sub
    Private Function ile_rekordow_BG() As Integer

        Dim dtDane As New DataSet("ksiazki_bg")

        Dim strSQL As String
        dtDane.Clear()
        strSQL = "select * from ksiazki_bg"
        Dim objadapter As New MySql.Data.MySqlClient.MySqlDataAdapter(strSQL, conBG)
        If conBG.State = ConnectionState.Open Then
            Try
                objadapter.Fill(dtDane, "ksiazki_bg")
                Return dtDane.Tables("ksiazki_bg").Rows.Count
            Catch ex As Exception
                MessageBox.Show("Odyczt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Function
    Private Function ile_rekordow_BW() As Integer

        Dim dtDane As New DataSet("ksiazki_bw")

        Dim strSQL As String
        dtDane.Clear()
        strSQL = "select * from ksiazki_bw"
        Dim objadapter As New MySql.Data.MySqlClient.MySqlDataAdapter(strSQL, conBW)
        If conBW.State = ConnectionState.Open Then
            Try
                objadapter.Fill(dtDane, "ksiazki_bw")
                Return dtDane.Tables("ksiazki_bw").Rows.Count
            Catch ex As Exception
                MessageBox.Show("Odyczt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Function
    Private Sub Przeglad_bglowna()

        czysc_wpis()

        Dim strSQLGatunek As String
        Dim dtDane As New DataSet("ksiazki_bg")
        Dim objAdapter As New MySqlDataAdapter

        dtDane.Clear()
        strSQLGatunek = "SELECT DISTINCT gatunek from ksiazki_bg"
        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLGatunek, conBG)
        objAdapter.Fill(dtDane, "ksiazki_bg")

        If conBG.State = ConnectionState.Open Then
            With CBGatunek
                .DataSource = dtDane.Tables("ksiazki_bg")
                .DisplayMember = "gatunek"
                .SelectedIndex = 0
            End With

        End If
    End Sub
    Private Sub Przeglad_Gatunek_BG()

        Dim strSQLAutor As String
        Dim dtDane As New DataSet("ksiazki_bg")
        Dim objAdapter As New MySqlDataAdapter

        dtDane.Clear()
        strSQLAutor = "SELECT DISTINCT autor from ksiazki_bg where gatunek = '" & CBGatunek.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLAutor, conBG)
        objAdapter.Fill(dtDane, "ksiazki_bg")

        If conBG.State = ConnectionState.Open Then
            With CBAutor
                .DataSource = dtDane.Tables("ksiazki_bg")
                .DisplayMember = "autor"
            End With
        End If

    End Sub
    Private Sub Przeglad_Autor_BG()
        Dim strSQLTytul As String
        Dim dtDane As New DataSet("ksiazki_bg")
        Dim objAdapter As New MySqlDataAdapter


        dtDane.Clear()
        strSQLTytul = "SELECT DISTINCT tytul from ksiazki_bg where autor = '" & CBAutor.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLTytul, conBG)
        objAdapter.Fill(dtDane, "ksiazki_bg")

        If conBG.State = ConnectionState.Open Then
            With CBTytul
                .DataSource = dtDane.Tables("ksiazki_bg")
                .DisplayMember = "tytul"
            End With
        End If
    End Sub

    Private Sub Przeglad_Tytul_BG()

        Dim strSQLNrPoz As String
        Dim dtDane As New DataSet("ksiazki_bg")
        Dim objAdapter As New MySqlDataAdapter


        dtDane.Clear()
        strSQLNrPoz = "SELECT DISTINCT nr_pozycji from ksiazki_bg where tytul = '" & CBTytul.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLNrPoz, conBG)
        objAdapter.Fill(dtDane, "ksiazki_bg")

        If conBG.State = ConnectionState.Open Then

            With CBNrPoz
                .DataSource = dtDane.Tables("ksiazki_bg")
                .DisplayMember = "nr_pozycji"
            End With
        End If

    End Sub
    Private Sub Przeglad_Pozycji_BG()

        Dim strSQLpozycja As String
        Dim dtDane As New DataSet("ksiazki_bg")
        Dim objAdapter As New MySqlDataAdapter
        Dim dTable As DataTable
        Dim dtRow As DataRow
        Dim pozycja As Integer
        Dim arrImage() As Byte


        dtDane.Clear()
        strSQLpozycja = "SELECT * from ksiazki_bg where nr_pozycji=" & CBNrPoz.Text

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLpozycja, conBG)
        objAdapter.Fill(dtDane, "ksiazki_bg")

        If conBG.State = ConnectionState.Open Then

            dTable = dtDane.Tables("ksiazki_bg")
            dtRow = dTable.Rows(pozycja)
            TxtWydawnictwo.Text = dtRow.Item(5)
            arrImage = dtRow.Item(6)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PB_Ksiazka.Image = Image.FromStream(mstream)
        End If

    End Sub
    Private Sub Przeglad_bwojewodzka()

        czysc_wpis()

        Dim strSQLGatunek As String
        Dim dtDane As New DataSet("ksiazki_bw")
        Dim objAdapter As New MySqlDataAdapter
        Dim dTable As New DataTable

        dtDane.Clear()
        strSQLGatunek = "SELECT DISTINCT gatunek from ksiazki_bw"
        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLGatunek, conBW)
        objAdapter.Fill(dtDane, "ksiazki_bw")

        If conBW.State = ConnectionState.Open Then
            With CBGatunek
                ' .Items.Add("Wybierz")
                .DataSource = dtDane.Tables("ksiazki_bw")
                .DisplayMember = "gatunek"
                .SelectedIndex = 0
            End With
        End If

    End Sub
    Private Sub Przeglad_Gatunek_BW()

        Dim strSQLAutor As String
        Dim dtDane As New DataSet("ksiazki_bw")
        Dim objAdapter As New MySqlDataAdapter

        dtDane.Clear()
        strSQLAutor = "SELECT DISTINCT autor from ksiazki_bw where gatunek = '" & CBGatunek.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLAutor, conBW)
        objAdapter.Fill(dtDane, "ksiazki_bw")

        If conBW.State = ConnectionState.Open Then
            With CBAutor
                .DataSource = dtDane.Tables("ksiazki_bw")
                .DisplayMember = "autor"
            End With
        End If

    End Sub
    Private Sub Przeglad_Autor_BW()
        Dim strSQLTytul As String
        Dim dtDane As New DataSet("ksiazki_bw")
        Dim objAdapter As New MySqlDataAdapter


        dtDane.Clear()
        strSQLTytul = "SELECT DISTINCT tytul from ksiazki_bw where autor = '" & CBAutor.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLTytul, conBW)
        objAdapter.Fill(dtDane, "ksiazki_bw")

        If conBW.State = ConnectionState.Open Then
            With CBTytul
                .DataSource = dtDane.Tables("ksiazki_bw")
                .DisplayMember = "tytul"
            End With
        End If
    End Sub

    Private Sub Przeglad_Tytul_BW()

        Dim strSQLNrPoz As String
        Dim dtDane As New DataSet("ksiazki_bw")
        Dim objAdapter As New MySqlDataAdapter


        dtDane.Clear()
        strSQLNrPoz = "SELECT DISTINCT nr_pozycji from ksiazki_bw where tytul = '" & CBTytul.Text & "'"

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLNrPoz, conBW)
        objAdapter.Fill(dtDane, "ksiazki_bw")

        If conBW.State = ConnectionState.Open Then

            With CBNrPoz
                .DataSource = dtDane.Tables("ksiazki_bw")
                .DisplayMember = "nr_pozycji"
            End With
        End If

    End Sub
    Private Sub Przeglad_Pozycji_BW()

        Dim strSQLpozycja As String
        Dim dtDane As New DataSet("ksiazki_bw")
        Dim objAdapter As New MySqlDataAdapter
        Dim dTable As DataTable
        Dim dtRow As DataRow
        Dim pozycja As Integer
        Dim arrImage() As Byte


        dtDane.Clear()
        strSQLpozycja = "SELECT * from ksiazki_bw where nr_pozycji=" & CBNrPoz.Text

        objAdapter = New MySql.Data.MySqlClient.MySqlDataAdapter(strSQLpozycja, conBW)
        objAdapter.Fill(dtDane, "ksiazki_bw")

        If conBW.State = ConnectionState.Open Then

            dTable = dtDane.Tables("ksiazki_bw")
            dtRow = dTable.Rows(pozycja)
            TxtWydawnictwo.Text = dtRow.Item(5)
            arrImage = dtRow.Item(6)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PB_Ksiazka.Image = Image.FromStream(mstream)
        End If

    End Sub

    Private Sub zaloguj_BG()

        Dim dTable As New DataTable
        Dim objadapter As New MySqlDataAdapter("select * from pracownik_bg where username ='" & TxtUser.Text & "' and userpassword = '" & TxtPass.Text & "'", conBG)
        dTable.Clear()
        objadapter.Fill(dTable)

        If dTable.Rows.Count > 0 Then
            Dim user_type, name As String
            user_type = dTable.Rows(0).Item(4)
            name = dTable.Rows(0).Item(2)
            If user_type = "Admin" Then
                MsgBox("Witaj, " & name & " zalogowałeś się jako Administrator")
                TxtUser.Text = ""
                TxtPass.Text = ""
                FBibliotekaPracownik.Show()
                FBibliotekaPracownik.TxtConnection.Text = "Witaj, " & name & " w Bibliotece Głównej"
                FBibliotekaPracownik.Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BG() & " książek"
            ElseIf user_type = "Pracownik" Then
                MsgBox("Witaj, " & name & " zalogowałeś się jako pracownik")
                TxtUser.Text = ""
                TxtPass.Text = ""
                FBibliotekaPracownik.Show()
                FBibliotekaPracownik.TxtConnection.Text = "Witaj, " & name & " w Bibliotece Głównej"
                FBibliotekaPracownik.Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BG() & " książek"
            End If
        End If

        If dTable.Rows.Count = Nothing Then
            MsgBox("Niepoprwany użytkownik lub hasło !")
        Else
            FBibliotekaPracownik.Show()
            'Me.Hide()
        End If
        conBG.Close()

    End Sub
    Private Sub zaloguj_BW()

        Dim dTable As New DataTable
        Dim objadapter As New MySqlDataAdapter("select * from pracownik_bw where username ='" & TxtUser.Text & "' and userpassword = '" & TxtPass.Text & "'", conBW)
        dTable.Clear()
        objadapter.Fill(dTable)

        If dTable.Rows.Count > 0 Then
            Dim user_type, name As String
            user_type = dTable.Rows(0).Item(4)
            name = dTable.Rows(0).Item(2)
            If user_type = "Admin" Then
                MsgBox("Witaj, " & name & " zalogowałeś się jako Administrator")
                TxtUser.Text = ""
                TxtPass.Text = ""
                FBibliotekaPracownik.Show()
                FBibliotekaPracownik.TxtConnection.Text = "Witaj, " & name & " w Wojewódzkiej Bibliotece Publicznej"
                FBibliotekaPracownik.Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BW() & " książek"
            ElseIf user_type = "Pracownik" Then
                MsgBox("Witaj, " & name & " zalogowałeś się jako pracownik")
                TxtUser.Text = ""
                TxtPass.Text = ""
                FBibliotekaPracownik.Show()
                FBibliotekaPracownik.TxtConnection.Text = "Witaj, " & name & " w Wojewódzkiej Bibliotece Publicznej"
                FBibliotekaPracownik.Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BW() & " książek"
            End If
        End If

        If dTable.Rows.Count = Nothing Then
            MsgBox("Nie wprowadziłeś użytkownika lub hasła !")
        Else
            FBibliotekaPracownik.Show()
            'Me.Hide()
        End If
        conBW.Close()
    End Sub

    Private Sub BtnEndTab_Click(sender As System.Object, e As System.EventArgs) Handles BtnEndTab.Click

        End

    End Sub



    Private Sub BtnChooseDB_Click(sender As System.Object, e As System.EventArgs) Handles BtnChooseDB.Click

        If CB_DataBase_Select.SelectedIndex = 0 Then
            polacz_z_BG()
            TxtConnection.Text = TxtConnection.Text & " Witaj w Bibliotece Głównej "
            Przeglad_bglowna()
        End If

        If CB_DataBase_Select.SelectedIndex = 1 Then
            polacz_z_BW()
            TxtConnection.Text = TxtConnection.Text & " Witaj w Wojewódzkiej Bibliotece Publicznej "
            Przeglad_bwojewodzka()
        End If

    End Sub

    Private Sub CBGatunek_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBGatunek.SelectedIndexChanged

        If CB_DataBase_Select.SelectedIndex = 0 Then

            Przeglad_Gatunek_BG()
        End If

        If CB_DataBase_Select.SelectedIndex = 1 Then

            Przeglad_Gatunek_BW()

        End If

    End Sub

    Private Sub CBAutor_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBAutor.SelectedIndexChanged

        If CB_DataBase_Select.SelectedIndex = 0 Then

            Przeglad_Autor_BG()
        End If

        If CB_DataBase_Select.SelectedIndex = 1 Then

            Przeglad_Autor_BW()

        End If

    End Sub

    Private Sub CBTytul_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBTytul.SelectedIndexChanged
        If CB_DataBase_Select.SelectedIndex = 0 Then

            Przeglad_Tytul_BG()

        End If

        If CB_DataBase_Select.SelectedIndex = 1 Then

            Przeglad_Tytul_BW()

        End If

    End Sub
    Private Sub CBNrPoz_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CBNrPoz.SelectedIndexChanged

        If CB_DataBase_Select.SelectedIndex = 0 Then

            Przeglad_Pozycji_BG()

        End If

        If CB_DataBase_Select.SelectedIndex = 1 Then

            Przeglad_Pozycji_BW()

        End If
    End Sub
    Private Sub BtnCancel2_Click(sender As Object, e As EventArgs) Handles BtnCancel2.Click
        End
    End Sub
    Private Sub BtnSelectBook_Click(sender As Object, e As EventArgs) Handles BtnSelectBook.Click

        If Not CB_DataBase_Select.SelectedIndex = 0 And Not CB_DataBase_Select.SelectedIndex = 1 Then

            MessageBox.Show("Wybierz Bibliotekę!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Else
            MessageBox.Show("Ksiązka została dodana do schowka!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If

    End Sub
    Private Sub BtnLogin_Click(sender As System.Object, e As System.EventArgs) Handles BtnLogin.Click

        If Not CB_DataBase_Select.SelectedIndex = 0 And Not CB_DataBase_Select.SelectedIndex = 1 Then

            MessageBox.Show("Wybierz Bibliotekę, do której chcesz się zalogować!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)

        Else

            If CB_DataBase_Select.SelectedIndex = 0 Then

                If conBG.State = ConnectionState.Closed Then
                    conBW.Close()
                    polacz_z_BG()
                End If
                zaloguj_BG()
            ElseIf CB_DataBase_Select.SelectedIndex = 1 Then
                If conBW.State = ConnectionState.Closed Then
                    conBG.Close()
                    polacz_z_BW()
                End If
                zaloguj_BW()
            End If
        End If

    End Sub
    Private Sub BtnCancel_Click(sender As System.Object, e As System.EventArgs) Handles BtnCancel.Click

        End

    End Sub

---------------------------------------------FORM 2 ---------------------------------------------------------------------------
Imports System.Data
Imports MySql.Data.MySqlClient
Imports System.IO

Public Class FBibliotekaPracownik

    Dim conBG As New MySql.Data.MySqlClient.MySqlConnection("server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekaglowna")
    Dim conBW As New MySql.Data.MySqlClient.MySqlConnection("server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekawojewodzka")
    Dim dtDaneBG As New DataSet("ksiazki_bg")
    Dim dtDaneBW As New DataSet("ksiazki_bw")
    Dim rekord_nr As Integer = 0
    Dim dtable As New DataTable
    Dim dtView As DataView
    Dim dtRow As DataRow
    Dim wiersz As Integer

    Private Sub polacz_z_BG()

        Dim myConnectionString As String
        myConnectionString = "server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekaglowna"
        ' TxtConnection.Clear()
        conBW.Close()
        Try
            conBG.ConnectionString = myConnectionString
            conBG.Open()
        Catch ex As Exception
            MessageBox.Show("Błąd połączenia z Bazą Danych", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub polacz_z_BW()

        Dim myConnectionString As String
        myConnectionString = "server=localhost;uid=root;pwd=Biblioteka1;database=bibliotekawojewodzka"
        ' TxtConnection.Clear()
        conBG.Close()
        Try
            conBW.ConnectionString = myConnectionString
            conBW.Open()
        Catch ex As Exception
            MessageBox.Show("Błąd połączenia z Bazą Danych", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub czysc_wpis()

        TxtGatunek.Text = ""
        TxtAutor.Text = ""
        TxtTytul.Text = ""
        TxtNrPoz.Text = ""
        TxtWydawnictwo.Text = ""
        PB_dodaj.Image = Nothing

    End Sub
    Private Sub wyswietl_rekord_BG(ByVal wiersz As Integer)

        Dim arrImage() As Byte

        If wiersz >= 0 Then
            dtable = dtDaneBG.Tables("ksiazki_bg")
            dtRow = dtable.Rows(wiersz)
            Txt_ksiazka_id.Text = dtRow.Item(0)
            TxtEditGatunek.Text = dtRow.Item(1)
            TxtEditAutor.Text = dtRow.Item(2)
            TxtEditTytul.Text = dtRow.Item(3)
            TxtEditNrPoz.Text = dtRow.Item(4)
            TxtEditWyd.Text = dtRow.Item(5)
            arrImage = dtRow.Item(6)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PB_Edytuj.Image = Image.FromStream(mstream)
        End If

    End Sub
    Private Sub odczyt_do_edycji_BG(ByVal rekord_nr As Integer)

        Dim strSQL As String
        Dim objadapter As MySqlDataAdapter
        strSQL = "select * from ksiazki_bg"

        objadapter = New MySqlDataAdapter(strSQL, conBG)
        If conBG.State = ConnectionState.Open Then
            Try
                dtDaneBG.Clear()
                objadapter.Fill(dtDaneBG, "ksiazki_bg")
            Catch ex As System.Exception
                MessageBox.Show("Odczyt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                conBG.Close()
            End Try
        End If

        If dtDaneBG.Tables("ksiazki_bg").Rows.Count = 0 Then
            MessageBox.Show("Baza pusta - brak rekordów", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            BtnPrevious.Enabled = False
            BtnNext.Enabled = False
            BtnDelete.Enabled = False
            BtnSaveChanges.Enabled = False
            wyswietl_rekord_BG(-1)
            Exit Sub
        Else
            BtnPrevious.Enabled = True
            BtnNext.Enabled = True
            BtnDelete.Enabled = True
            BtnSaveChanges.Enabled = True
            wyswietl_rekord_BG(rekord_nr)
        End If

    End Sub
    Private Sub wyswietl_rekord_BW(ByVal wiersz As Integer)

        Dim arrImage() As Byte

        If wiersz >= 0 Then
            dtable = dtDaneBW.Tables("ksiazki_bw")
            dtRow = dtable.Rows(wiersz)
            Txt_ksiazka_id.Text = dtRow.Item(0)
            TxtEditGatunek.Text = dtRow.Item(1)
            TxtEditAutor.Text = dtRow.Item(2)
            TxtEditTytul.Text = dtRow.Item(3)
            TxtEditNrPoz.Text = dtRow.Item(4)
            TxtEditWyd.Text = dtRow.Item(5)
            arrImage = dtRow.Item(6)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PB_Edytuj.Image = Image.FromStream(mstream)
        End If
    End Sub
    Private Sub odczyt_do_edycji_BW(ByVal rekord_nr As Integer)

        Dim strSQL As String
        Dim objadapter As MySqlDataAdapter
        strSQL = "select * from ksiazki_bw"

        objadapter = New MySqlDataAdapter(strSQL, conBW)
        If conBW.State = ConnectionState.Open Then
            Try
                dtDaneBW.Clear()
                objadapter.Fill(dtDaneBW, "ksiazki_bw")
            Catch ex As System.Exception
                MessageBox.Show("Odczyt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                conBW.Close()
            End Try
        End If

        If dtDaneBW.Tables("ksiazki_bw").Rows.Count = 0 Then
            MessageBox.Show("Baza pusta - brak rekordów", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            BtnPrevious.Enabled = False
            BtnNext.Enabled = False
            BtnDelete.Enabled = False
            BtnSaveChanges.Enabled = False
            wyswietl_rekord_BW(-1)
            Exit Sub
        Else
            BtnPrevious.Enabled = True
            BtnNext.Enabled = True
            BtnDelete.Enabled = True
            BtnSaveChanges.Enabled = True
            wyswietl_rekord_BW(rekord_nr)
        End If

    End Sub
    Private Function ile_rekordow_BG() As Integer

        Dim dtDane As New DataSet("ksiazki_bg")

        Dim strSQL As String
        dtDane.Clear()
        strSQL = "select * from ksiazki_bg"
        Dim objadapter As New MySql.Data.MySqlClient.MySqlDataAdapter(strSQL, conBG)
        If conBG.State = ConnectionState.Open Then
            Try
                objadapter.Fill(dtDane, "ksiazki_bg")
                Return dtDane.Tables("ksiazki_bg").Rows.Count
            Catch ex As Exception
                MessageBox.Show("Odyczt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Function

    Private Function ile_rekordow_BW() As Integer

        Dim dtDane As New DataSet("ksiazki_bw")

        Dim strSQL As String
        dtDane.Clear()
        strSQL = "select * from ksiazki_bw"
        Dim objadapter As New MySql.Data.MySqlClient.MySqlDataAdapter(strSQL, conBW)
        If conBW.State = ConnectionState.Open Then
            Try
                objadapter.Fill(dtDane, "ksiazki_bw")
                Return dtDane.Tables("ksiazki_bw").Rows.Count
            Catch ex As Exception
                MessageBox.Show("Odyczt z bazy nie jest możliwy", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Function


    Private Sub BtnBrowse_Click(sender As Object, e As EventArgs) Handles BtnBrowse.Click

        Try
            With OpenFileDialog1
                .CheckFileExists = True
                .CheckPathExists = True
                .DefaultExt = "jpg"
                .DereferenceLinks = True
                .FileName = ""
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                .Multiselect = False
                .RestoreDirectory = True
                .Title = "Select a file to open"
                .ValidateNames = True
                If .ShowDialog = DialogResult.OK Then
                    Try
                        PB_dodaj.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try

    End Sub
    Private Sub zapisz_BG()

        Dim cmd As MySqlCommand
        Dim strWpis As String
        Dim result As Integer
        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()
        PB_dodaj.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        arrImage = mstream.GetBuffer()
        Dim FileSize = mstream.Length
        mstream.Close()

        Try
            If conBG.State = ConnectionState.Closed Then
                polacz_z_BG()
            End If
            strWpis = "INSERT INTO ksiazki_bg (gatunek, autor, tytul, nr_pozycji, wydawnictwo, ksiazka_img) VALUES (@gatunek, @autor, @tytul, @nr_pozycji, @wydawnictwo, @ksiazka_img )"
            cmd = New MySqlCommand
            With cmd
                .Connection = conBG
                .CommandText = strWpis
                .Parameters.AddWithValue("@gatunek", TxtGatunek.Text)
                .Parameters.AddWithValue("@autor", TxtAutor.Text)
                .Parameters.AddWithValue("@tytul", TxtTytul.Text)
                .Parameters.AddWithValue("@nr_pozycji", TxtNrPoz.Text)
                .Parameters.AddWithValue("@wydawnictwo", TxtWydawnictwo.Text)
                .Parameters.AddWithValue("@ksiazka_img", arrImage)
                result = .ExecuteNonQuery()
            End With
            If result > 0 Then
                Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BG() & " książek"
                MsgBox("Rekord został dodany do Bazy Danych")
            Else
                MsgBox("Error query", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conBG.Close()
        End Try

    End Sub
    Private Sub zapisz_BW()

        Dim cmd As MySqlCommand
        Dim strWpis As String
        Dim result As Integer
        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()
        PB_dodaj.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        arrImage = mstream.GetBuffer()
        Dim FileSize = mstream.Length
        mstream.Close()

        Try
            If conBW.State = ConnectionState.Closed Then
                polacz_z_BW()
                Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BW() & " książek"
            End If
            strWpis = "INSERT INTO ksiazki_bw (gatunek, autor, tytul, nr_pozycji, wydawnictwo, ksiazka_img) VALUES (@gatunek, @autor, @tytul, @nr_pozycji, @wydawnictwo, @ksiazka_img )"
            cmd = New MySqlCommand
            With cmd
                .Connection = conBW
                .CommandText = strWpis
                .Parameters.AddWithValue("@gatunek", TxtGatunek.Text)
                .Parameters.AddWithValue("@autor", TxtAutor.Text)
                .Parameters.AddWithValue("@tytul", TxtTytul.Text)
                .Parameters.AddWithValue("@nr_pozycji", TxtNrPoz.Text)
                .Parameters.AddWithValue("@wydawnictwo", TxtWydawnictwo.Text)
                .Parameters.AddWithValue("@ksiazka_img", arrImage)
                result = .ExecuteNonQuery()
            End With
            If result > 0 Then
                Lbl_licz_rekord.Text = "W bazie znajduje się " & ile_rekordow_BW() & " książek"
                MsgBox("Rekord został dodany do Bazy Danych")
            Else
                MsgBox("Error query", MsgBoxStyle.Exclamation)
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conBW.Close()
        End Try

    End Sub
    Private Sub edytuj_BG()

        Dim cmd As New MySqlCommand
        Dim strEdit As String
        Dim result As Integer
        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()
        PB_Edytuj.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        arrImage = mstream.GetBuffer()
        Dim FileSize = mstream.Length
        mstream.Close()

        If conBG.State = ConnectionState.Closed Then
            polacz_z_BG()
        End If

        strEdit = "UPDATE ksiazki_bg SET gatunek=@gatunek, autor=@autor, tytul=@tytul, nr_pozycji=@nr_pozycji, wydawnictwo=@wydawnictwo, ksiazka_img=@ksiazka_img WHERE ksiazka_id =" & Txt_ksiazka_id.Text & ""

        If conBG.State = ConnectionState.Open Then

            Try

                cmd = New MySqlCommand
                With cmd
                    .Connection = conBG
                    .CommandText = strEdit
                    .Parameters.AddWithValue("@gatunek", TxtEditGatunek.Text)
                    .Parameters.AddWithValue("@autor", TxtEditAutor.Text)
                    .Parameters.AddWithValue("@tytul", TxtEditTytul.Text)
                    .Parameters.AddWithValue("@nr_pozycji", TxtEditNrPoz.Text)
                    .Parameters.AddWithValue("@wydawnictwo", TxtEditWyd.Text)
                    .Parameters.AddWithValue("@ksiazka_img", arrImage)
                    result = .ExecuteNonQuery()
                    odczyt_do_edycji_BG(rekord_nr)
                End With
                MessageBox.Show("Rekord został zaktualizowany", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Bład podczas aktualizacji danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                conBG.Close()
            End Try
        End If

    End Sub
    Private Sub edytuj_BW()

        Dim cmd As New MySqlCommand
        Dim strEdit As String
        Dim result As Integer
        Dim arrImage() As Byte
        Dim mstream As New System.IO.MemoryStream()
        PB_Edytuj.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

        arrImage = mstream.GetBuffer()
        Dim FileSize = mstream.Length
        mstream.Close()

        If conBW.State = ConnectionState.Closed Then
            polacz_z_BW()
        End If

        strEdit = "UPDATE ksiazki_bw SET gatunek=@gatunek, autor=@autor, tytul=@tytul, nr_pozycji=@nr_pozycji, wydawnictwo=@wydawnictwo, ksiazka_img=@ksiazka_img WHERE ksiazka_id =" & Txt_ksiazka_id.Text & ""

        If conBW.State = ConnectionState.Open Then

            Try

                cmd = New MySqlCommand
                With cmd
                    .Connection = conBW
                    .CommandText = strEdit
                    .Parameters.AddWithValue("@gatunek", TxtEditGatunek.Text)
                    .Parameters.AddWithValue("@autor", TxtEditAutor.Text)
                    .Parameters.AddWithValue("@tytul", TxtEditTytul.Text)
                    .Parameters.AddWithValue("@nr_pozycji", TxtEditNrPoz.Text)
                    .Parameters.AddWithValue("@wydawnictwo", TxtEditWyd.Text)
                    .Parameters.AddWithValue("@ksiazka_img", arrImage)
                    result = .ExecuteNonQuery()
                    odczyt_do_edycji_BW(rekord_nr)
                End With
                MessageBox.Show("Rekord został zaktualizowany", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Bład podczas aktualizacji danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Finally
                conBW.Close()
            End Try
        End If

    End Sub
    Private Sub usun_BG()

        polacz_z_BG()
        If conBG.State = ConnectionState.Open Then
            If ile_rekordow_BG() > 0 Then
                Dim pytanie As DialogResult
                pytanie = MessageBox.Show("Czy chcesz skasować bieżący rekord?", "Usuwanie danych", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If pytanie = DialogResult.Yes Then
                    Dim strSQL As String
                    strSQL = "DELETE FROM ksiazki_bg WHERE ksiazka_id= " & Txt_ksiazka_id.Text
                    Try
                        Dim objadapter As New MySql.Data.MySqlClient.MySqlCommand(strSQL, conBG)
                        objadapter.ExecuteNonQuery()
                        MessageBox.Show("Rekord został usunięty", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        odczyt_do_edycji_BG(0)
                    Catch ex As Exception
                        MessageBox.Show("Bład podczas aktualizacji danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        conBG.Close()
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Brak połaczenia z bazą", "Bład", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub usun_BW()

        polacz_z_BW()

        If conBW.State = ConnectionState.Open Then
            If ile_rekordow_BW() > 0 Then
                Dim pytanie As DialogResult
                pytanie = MessageBox.Show("Czy chcesz skasować bieżący rekord?", "Usuwanie danych", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2)
                If pytanie = DialogResult.Yes Then
                    Dim strSQL As String
                    strSQL = "DELETE FROM ksiazki_bw WHERE ksiazka_id= " & Txt_ksiazka_id.Text
                    Try
                        Dim objadapter As New MySql.Data.MySqlClient.MySqlCommand(strSQL, conBW)
                        objadapter.ExecuteNonQuery()
                        MessageBox.Show("Rekord został usunięty", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        odczyt_do_edycji_BW(0)
                    Catch ex As Exception
                        MessageBox.Show("Bład podczas aktualizacji danych", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Finally
                        conBW.Close()
                    End Try
                End If
            End If
        Else
            MessageBox.Show("Brak połaczenia z bazą", "Bład", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If

    End Sub
    Private Sub BtnSave_Click(sender As System.Object, e As System.EventArgs) Handles BtnSave.Click

        If TxtGatunek.Text.Length = 0 Then
            MessageBox.Show("Proszę uzupełnić brakujące dane", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtGatunek.Focus()
            Exit Sub
        End If

        If TxtAutor.Text.Length = 0 Then
            MessageBox.Show("Proszę uzupełnić brakujące dane", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtAutor.Focus()
            Exit Sub
        End If

        If TxtTytul.Text.Length = 0 Then
            MessageBox.Show("Proszę uzupełnić brakujące dane", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtTytul.Focus()
            Exit Sub
        End If

        If TxtNrPoz.Text.Length = 0 OrElse Not IsNumeric(TxtNrPoz.Text) Then
            MessageBox.Show("Proszę uzupełnić brakujące dane, nr_pozycji", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtNrPoz.Focus()
            Exit Sub
        End If

        If TxtWydawnictwo.Text.Length = 0 Then
            MessageBox.Show("Proszę uzupełnić brakujące dane", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtWydawnictwo.Focus()
            Exit Sub
        End If

        If PB_dodaj.Image Is Nothing Then
            MessageBox.Show("Proszę uzupełnić brakujące dane, brak obrazka", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtWydawnictwo.Focus()
            Exit Sub
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then
            If conBG.State = ConnectionState.Closed Then
                conBW.Close()
                polacz_z_BG()
            End If
            zapisz_BG()
            czysc_wpis()
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then
            If conBW.State = ConnectionState.Closed Then
                conBG.Close()
                polacz_z_BW()
            End If
            zapisz_BW()
            czysc_wpis()
        End If

    End Sub
    Private Sub Btn_Browse_Click(sender As Object, e As EventArgs) Handles Btn_Browse.Click
        Try
            With OpenFileDialog1
                .CheckFileExists = True
                .CheckPathExists = True
                .DefaultExt = "jpg"
                .DereferenceLinks = True
                .FileName = ""
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                .Multiselect = False
                .RestoreDirectory = True
                .Title = "Select a file to open"
                .ValidateNames = True
                If .ShowDialog = DialogResult.OK Then
                    Try
                        PB_Edytuj.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub
    Private Sub BtnClear_Click(sender As Object, e As EventArgs) Handles BtnClear.Click
        czysc_wpis()
    End Sub
    Private Sub TabKatalog_SelectedIndexChanged(sender As Object, e As EventArgs) Handles TabEdit.SelectedIndexChanged

        If TabEdit.SelectedIndex = 1 And FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            polacz_z_BG()
            rekord_nr = 0
            odczyt_do_edycji_BG(0)

        End If

        If TabEdit.SelectedIndex = 1 And FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            polacz_z_BW()
            rekord_nr = 0
            odczyt_do_edycji_BW(0)

        End If

    End Sub


    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles BtnPrevious.Click

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            If rekord_nr > 0 Then
                rekord_nr -= 1
                wyswietl_rekord_BG(rekord_nr)
            Else
                MessageBox.Show("Pierwszy rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            If rekord_nr > 0 Then
                rekord_nr -= 1
                wyswietl_rekord_BW(rekord_nr)
            Else
                MessageBox.Show("Pierwszy rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles BtnNext.Click

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then
            If rekord_nr < dtDaneBG.Tables("ksiazki_bg").Rows.Count - 1 Then
                rekord_nr += 1
                wyswietl_rekord_BG(rekord_nr)
            Else
                MessageBox.Show("Ostatni rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then
            If rekord_nr < dtDaneBW.Tables("ksiazki_bw").Rows.Count - 1 Then
                rekord_nr += 1
                wyswietl_rekord_BW(rekord_nr)
            Else
                MessageBox.Show("Ostatni rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub
    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles BtnFirst.Click

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            If rekord_nr > 0 Then
                rekord_nr = 0
                wyswietl_rekord_BG(rekord_nr)
            Else
                MessageBox.Show("Pierwszy rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            If rekord_nr > 0 Then
                rekord_nr = 0
                wyswietl_rekord_BW(rekord_nr)
            Else
                MessageBox.Show("Ostatni rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If
    End Sub

    Private Sub BtnLast_Click(sender As Object, e As EventArgs) Handles BtnLast.Click

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            If rekord_nr >= 0 Then
                rekord_nr = dtDaneBG.Tables("ksiazki_bg").Rows.Count - 1
                wyswietl_rekord_BG(rekord_nr)
            Else
                MessageBox.Show("Ostatni rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            If rekord_nr >= 0 Then
                rekord_nr = dtDaneBW.Tables("ksiazki_bw").Rows.Count - 1
                wyswietl_rekord_BW(rekord_nr)
            Else
                MessageBox.Show("Ostatni rekord - przejście dalej nie jest możliwe", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        End If

    End Sub

    Private Sub BtnSaveChanges_Click(sender As Object, e As EventArgs) Handles BtnSaveChanges.Click

        If Txt_ksiazka_id.Text.Length = 0 OrElse Not IsNumeric(Txt_ksiazka_id.Text) Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Txt_ksiazka_id.Focus()
            Exit Sub
        End If

        If TxtEditGatunek.Text.Length = 0 Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtEditGatunek.Focus()
            Exit Sub
        End If
        If TxtEditAutor.Text.Length = 0 Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtEditAutor.Focus()
            Exit Sub
        End If
        If TxtEditTytul.Text.Length = 0 Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtEditTytul.Focus()
            Exit Sub
        End If
        If TxtEditNrPoz.Text.Length = 0 OrElse Not IsNumeric(TxtEditNrPoz.Text) Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtEditNrPoz.Focus()
            Exit Sub
        End If

        If TxtEditWyd.Text.Length = 0 Then
            MessageBox.Show("Błędny wpis", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtEditWyd.Focus()
            Exit Sub
        End If

        If PB_Edytuj.Image Is Nothing Then
            MessageBox.Show("Błędny wpis", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Error)
            PB_Edytuj.Focus()
            Exit Sub
        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            edytuj_BG()

        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            edytuj_BW()

        End If

    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs) Handles BtnDelete.Click

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 0 Then

            usun_BG()

        End If

        If FBiblioteka.CB_DataBase_Select.SelectedIndex = 1 Then

            usun_BW()

        End If

    End Sub


End Class