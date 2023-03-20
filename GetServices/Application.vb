
Module Program
    Public Function Method() As Boolean


        Dim ServicesURL As String = "https://tradestie.com/api/v1/apps/reddit"

        Try


            'Fetching the services definition
            Dim webRequest As Object = TryCast(System.Net.WebRequest.Create(ServicesURL), System.Net.HttpWebRequest)
            webRequest.KeepAlive = True
            webRequest.Method = "GET"


            Dim response As System.Net.HttpWebResponse = TryCast(webRequest.GetResponse(), System.Net.HttpWebResponse)
            Dim readerServices As New System.IO.StreamReader(response.GetResponseStream())
            Dim services As String = readerServices.ReadToEnd()
            'System.Console.WriteLine("Teste" & services)

            If String.IsNullOrEmpty(services) Then
                System.Console.WriteLine("dentro do if")
                Return False
            End If

            Dim servicesObj As Object = Newtonsoft.Json.JsonConvert.DeserializeObject(services)
            'System.Console.WriteLine("Resposta Formatada:" & servicesObj.ToString)
            ' System.Console.WriteLine(servicesObj.Count)

            Dim message As String = ""


            For i As Integer = 0 To servicesObj.Count - 1
                Dim item As Object = servicesObj(i)
                Dim numero_de_comentarios As String = item("no_of_comments").ToString()
                Dim sentimento As String = item("sentiment").ToString()
                Dim sentimento_score As String = item("sentiment_score").ToString()
                Dim codigo_de_negoc As String = item("ticker").ToString()
                If i > 0 Then
                    message = message & System.Environment.NewLine

                End If

                message = message & i + 1 & ") Número de Comentários: " & numero_de_comentarios & " - Sentimento: " & sentimento &
                " - Sentimento Score: " & sentimento_score & " - Código de Negociação: " & codigo_de_negoc
            Next

            System.Console.WriteLine("Resposta Formatada : " & System.Environment.NewLine & message)
            System.Console.WriteLine("Quantidade de Ações : " & servicesObj.Count)

            Return True
        Catch ex As System.Exception
            Return False

        End Try

    End Function

    Sub Main()
        Dim Value As Boolean = Method()
        System.Console.WriteLine("-------------------------------")
        System.Console.WriteLine("Retorno " & Value)

    End Sub

End Module
