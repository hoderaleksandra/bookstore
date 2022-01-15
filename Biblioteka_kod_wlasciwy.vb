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

