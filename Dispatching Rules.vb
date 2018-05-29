satir = BunifuCustomDataGrid1.Rows.Count
        Pi_toplam = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        pp(0) = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        cc(0) = pp(0)
        For i = 0 To satir - 1
            noo(i) = BunifuCustomDataGrid1.Rows(i).Cells(0).Value
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            dd(i) = BunifuCustomDataGrid1.Rows(i).Cells(2).Value
            ll(i) = BunifuCustomDataGrid1.Rows(i).Cells(3).Value
            ee(i) = BunifuCustomDataGrid1.Rows(i).Cells(4).Value

        Next i
        For i = 1 To satir - 1
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            cc(i) = cc(i - 1) + pp(i)

        Next

        For i = 1 To satir - 1

            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value

            Pi_toplam = Pi_toplam + pp(i)
        Next i

        For sayaç = 0 To satir - 3
            For sayaç2 = sayaç + 1 To satir - 2
                If pp(sayaç2) < pp(sayaç) Then
                    gecici = pp(sayaç)
                    gecici2 = noo(sayaç)
                    gecici3 = dd(sayaç)
                    gecici4 = ee(sayaç)
                    gecici5 = ll(sayaç)
                    gecici6 = cc(sayaç)
                    cc(sayaç) = cc(sayaç2)
                    cc(sayaç2) = gecici6
                    ll(sayaç) = ll(sayaç2)
                    ll(sayaç2) = gecici5
                    ee(sayaç) = ee(sayaç2)
                    ee(sayaç2) = gecici4
                    dd(sayaç) = dd(sayaç2)
                    dd(sayaç2) = gecici3
                    noo(sayaç) = noo(sayaç2)
                    noo(sayaç2) = gecici2
                    pp(sayaç) = pp(sayaç2)
                    pp(sayaç2) = gecici

                End If
            Next
        Next
        cc(0) = pp(0)
        For i = 1 To satir - 1

            cc(i) = cc(i - 1) + pp(i)

        Next

        series1spt.ValueScaleType = ScaleType.Numerical

        For sayaç = 0 To satir - 2

            sptform.spt.Rows.Add(noo(sayaç), pp(sayaç), dd(sayaç), cc(sayaç))




            series1spt.Points.Add(New SeriesPoint("SPT", (cc(sayaç) - pp(sayaç)), cc(sayaç)))



        Next


        sptform.sptgant.Series.AddRange(New Series() {series1spt})
        myView2spt.Color = Color.Aqua
        myView2spt.ColorEach = True
        myView2spt.MaxValueMarkerVisibility = True



        series1spt.Label.BackColor = Color.Yellow
        series1spt.Label.LineVisibility = True
        series1spt.Label.TextColor = Color.Azure
        myView2spt.MaxValueMarkerVisibility = DefaultBoolean.True
        myView2spt.MaxValueMarker.Color = Color.GreenYellow
        myView2spt.MaxValueMarker.Kind = MarkerKind.Star
        myView2spt.MaxValueMarker.StarPointCount = 5
        myView2spt.MaxValueMarker.Size = 10

        myView2spt.MinValueMarkerVisibility = DefaultBoolean.True
        myView2spt.MinValueMarker.Color = Color.GreenYellow
        myView2spt.MinValueMarker.Kind = MarkerKind.Circle
        myView2spt.MinValueMarker.Size = 10

        myView2spt.BarWidth = 0.5

        sptform.sptgant.Titles.Add(New ChartTitle())
        sptform.sptgant.Titles(0).Text = "Gantt Þemasý"







        sptform.Label2.Text = Pi_toplam

        sptform.sptgant.Visible = True
        gecikenis = 0
        For i = 0 To satir - 2
            If cc(i) - dd(i) <= 0 Then sptform.spt.Rows(i).Cells(5).Value = (dd(i) - cc(i)) * ee(i)
            If cc(i) - dd(i) > 0 Then sptform.spt.Rows(i).Cells(4).Value = (cc(i) - dd(i)) * ll(i)
            If cc(i) - dd(i) > 0 Then gecikenis = gecikenis + 1
        Next
        sptform.Label10.Text = gecikenis
        gecikmee = sptform.spt.Rows(0).Cells(4).Value
        For i = 1 To satir - 2
            gecikmee = gecikmee + sptform.spt.Rows(i).Cells(4).Value
        Next
        sptform.Label7.Text = gecikmee
        agirliksizgecikme = 0
        For i = 0 To satir - 2

            If cc(i) - dd(i) > 0 Then agirliksizgecikme = agirliksizgecikme + (cc(i) - dd(i))
        Next
        sptform.Label9.Text = agirliksizgecikme


        For i = 0 To satir - 2
            cezaligecikme(i) = sptform.spt.Rows(i).Cells(4).Value


        Next i
        For i = 0 To satir - 2

            gecikme(i) = cc(i) - dd(i)
            If gecikme(i) < 0 Then gecikme(i) = 0


        Next i
        For sayaç = 0 To satir - 2


            series2spt.Points.Add(New SeriesPoint(noo(sayaç), cezaligecikme(sayaç)))
            series3spt.Points.Add(New SeriesPoint(noo(sayaç), gecikme(sayaç)))
        Next
        sptform.sptgecikmepie.Series.AddRange(New Series() {series2spt})
        sptform.sptgecikmepie.Titles.Add(New ChartTitle())
        sptform.sptgecikmepie.Titles(0).Text = "Ceza Oranlarý"
        sptform.sptcezapie.Series.AddRange(New Series() {series3spt})
        sptform.sptcezapie.Titles.Add(New ChartTitle())
        sptform.sptcezapie.Titles(0).Text = "Gecikme Oranlarý"
        series2spt.Label.TextPattern = "{A}: {VP:p0}"
        CType(series2spt.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series2spt.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
        myview3spt.Titles.Add(New SeriesTitle())
        myview3spt.Titles(0).Text = series2spt.Name
        myview3spt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview3spt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview3spt.ExplodeMode = PieExplodeMode.UseFilters
        myview3spt.ExplodedDistancePercentage = 30


        CType(series3spt.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series3spt.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default

        myview4spt.Titles.Add(New SeriesTitle())
        myview4spt.Titles(0).Text = series3spt.Name
        myview4spt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview4spt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview4spt.ExplodeMode = PieExplodeMode.UseFilters
        myview4spt.ExplodedDistancePercentage = 30



        ' Hide the legend (if necessary).






        satir = BunifuCustomDataGrid1.Rows.Count
        Pi_toplam = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        pp(0) = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        cc(0) = pp(0)
        For i = 0 To satir - 1
            noo(i) = BunifuCustomDataGrid1.Rows(i).Cells(0).Value
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            dd(i) = BunifuCustomDataGrid1.Rows(i).Cells(2).Value
            ll(i) = BunifuCustomDataGrid1.Rows(i).Cells(3).Value
            ee(i) = BunifuCustomDataGrid1.Rows(i).Cells(4).Value

        Next i
        For i = 1 To satir - 1
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            cc(i) = cc(i - 1) + pp(i)

        Next

        For i = 1 To satir - 1

            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value

            Pi_toplam = Pi_toplam + pp(i)
        Next i

        For sayaç = 0 To satir - 3
            For sayaç2 = sayaç + 1 To satir - 2
                If dd(sayaç2) < dd(sayaç) Then
                    gecici = pp(sayaç)
                    gecici2 = noo(sayaç)
                    gecici3 = dd(sayaç)
                    gecici4 = ee(sayaç)
                    gecici5 = ll(sayaç)
                    gecici6 = cc(sayaç)
                    cc(sayaç) = cc(sayaç2)
                    cc(sayaç2) = gecici6
                    ll(sayaç) = ll(sayaç2)
                    ll(sayaç2) = gecici5
                    ee(sayaç) = ee(sayaç2)
                    ee(sayaç2) = gecici4
                    dd(sayaç) = dd(sayaç2)
                    dd(sayaç2) = gecici3
                    noo(sayaç) = noo(sayaç2)
                    noo(sayaç2) = gecici2
                    pp(sayaç) = pp(sayaç2)
                    pp(sayaç2) = gecici

                End If
            Next
        Next
        cc(0) = pp(0)
        For i = 1 To satir - 1

            cc(i) = cc(i - 1) + pp(i)

        Next
        series1edd.ValueScaleType = ScaleType.Numerical

        For sayaç = 0 To satir - 2

            eddform.edd.Rows.Add(noo(sayaç), pp(sayaç), dd(sayaç), cc(sayaç))



            series1edd.Points.Add(New SeriesPoint("EDD", (cc(sayaç) - pp(sayaç)), cc(sayaç)))



        Next


        eddform.eddgant.Series.AddRange(New Series() {series1edd})
        myView2edd.Color = Color.Aqua
        myView2edd.ColorEach = True
        myView2edd.MaxValueMarkerVisibility = True


        series1edd.Label.BackColor = Color.Yellow
        series1edd.Label.LineVisibility = True
        series1edd.Label.TextColor = Color.Azure
        myView2edd.MaxValueMarkerVisibility = DefaultBoolean.True
        myView2edd.MaxValueMarker.Color = Color.GreenYellow
        myView2edd.MaxValueMarker.Kind = MarkerKind.Star
        myView2edd.MaxValueMarker.StarPointCount = 5
        myView2edd.MaxValueMarker.Size = 10

        myView2edd.MinValueMarkerVisibility = DefaultBoolean.True
        myView2edd.MinValueMarker.Color = Color.GreenYellow
        myView2edd.MinValueMarker.Kind = MarkerKind.Circle
        myView2edd.MinValueMarker.Size = 10

        myView2edd.BarWidth = 0.5

        eddform.eddgant.Titles.Add(New ChartTitle())
        eddform.eddgant.Titles(0).Text = "Gantt Þemasý"



        eddform.Label2.Text = Pi_toplam

        eddform.eddgant.Visible = True
        gecikenis = 0
        For i = 0 To satir - 2
            If cc(i) - dd(i) <= 0 Then eddform.edd.Rows(i).Cells(5).Value = (dd(i) - cc(i)) * ee(i)
            If cc(i) - dd(i) > 0 Then eddform.edd.Rows(i).Cells(4).Value = (cc(i) - dd(i)) * ll(i)
            If cc(i) - dd(i) > 0 Then gecikenis = gecikenis + 1
        Next
        eddform.Label10.Text = gecikenis
        gecikmee = eddform.edd.Rows(0).Cells(4).Value
        For i = 1 To satir - 2
            gecikmee = gecikmee + eddform.edd.Rows(i).Cells(4).Value
        Next
        eddform.Label7.Text = gecikmee
        agirliksizgecikme = 0
        For i = 0 To satir - 2

            If cc(i) - dd(i) > 0 Then agirliksizgecikme = agirliksizgecikme + (cc(i) - dd(i))
        Next
        eddform.Label9.Text = agirliksizgecikme


        For i = 0 To satir - 2
            cezaligecikme(i) = eddform.edd.Rows(i).Cells(4).Value


        Next i
        For i = 0 To satir - 2

            gecikme(i) = cc(i) - dd(i)
            If gecikme(i) < 0 Then gecikme(i) = 0


        Next i
        For sayaç = 0 To satir - 2



            series2edd.Points.Add(New SeriesPoint(noo(sayaç), cezaligecikme(sayaç)))
            series3edd.Points.Add(New SeriesPoint(noo(sayaç), gecikme(sayaç)))
        Next
        eddform.eddgecikmepie.Series.AddRange(New Series() {series2edd})
        eddform.eddgecikmepie.Titles.Add(New ChartTitle())
        eddform.eddgecikmepie.Titles(0).Text = "Ceza Oranlarý"
        eddform.eddcezapie.Series.AddRange(New Series() {series3edd})
        eddform.eddcezapie.Titles.Add(New ChartTitle())
        eddform.eddcezapie.Titles(0).Text = "Gecikme Oranlarý"
        series2edd.Label.TextPattern = "{A}: {VP:p0}"
        CType(series2edd.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series2edd.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
        myview3edd.Titles.Add(New SeriesTitle())
        myview3edd.Titles(0).Text = series2edd.Name
        myview3edd.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview3edd.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview3edd.ExplodeMode = PieExplodeMode.UseFilters
        myview3edd.ExplodedDistancePercentage = 30

        CType(series3edd.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series3edd.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default

        myview4edd.Titles.Add(New SeriesTitle())
        myview4edd.Titles(0).Text = series3edd.Name
        myview4edd.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview4edd.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview4edd.ExplodeMode = PieExplodeMode.UseFilters
        myview4edd.ExplodedDistancePercentage = 30

        ' Hide the legend (if necessary).





        satir = BunifuCustomDataGrid1.Rows.Count
        Pi_toplam = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        pp(0) = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        cc(0) = pp(0)
        For i = 0 To satir - 1
            noo(i) = BunifuCustomDataGrid1.Rows(i).Cells(0).Value
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            dd(i) = BunifuCustomDataGrid1.Rows(i).Cells(2).Value
            ll(i) = BunifuCustomDataGrid1.Rows(i).Cells(3).Value
            ee(i) = BunifuCustomDataGrid1.Rows(i).Cells(4).Value

        Next i
        For i = 1 To satir - 1
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            cc(i) = cc(i - 1) + pp(i)

        Next

        For i = 1 To satir - 1

            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value

            Pi_toplam = Pi_toplam + pp(i)
        Next i


        cc(0) = pp(0)
        For i = 1 To satir - 1
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            cc(i) = cc(i - 1) + pp(i)

        Next
        series1fcfs.ValueScaleType = ScaleType.Numerical

        For sayaç = 0 To satir - 2

            Fcfsform.mevcut.Rows.Add(noo(sayaç), pp(sayaç), dd(sayaç), cc(sayaç))



            series1fcfs.Points.Add(New SeriesPoint("FCFS", (cc(sayaç) - pp(sayaç)), cc(sayaç)))

        Next


        Fcfsform.fcfsgant.Series.AddRange(New Series() {series1fcfs})
        myView2fcfs.Color = Color.Aqua
        myView2fcfs.ColorEach = True
        myView2fcfs.MaxValueMarkerVisibility = True


        series1fcfs.Label.BackColor = Color.Yellow
        series1fcfs.Label.LineVisibility = True
        series1fcfs.Label.TextColor = Color.Azure
        myView2fcfs.MaxValueMarkerVisibility = DefaultBoolean.True
        myView2fcfs.MaxValueMarker.Color = Color.GreenYellow
        myView2fcfs.MaxValueMarker.Kind = MarkerKind.Star
        myView2fcfs.MaxValueMarker.StarPointCount = 5
        myView2fcfs.MaxValueMarker.Size = 10

        myView2fcfs.MinValueMarkerVisibility = DefaultBoolean.True
        myView2fcfs.MinValueMarker.Color = Color.GreenYellow
        myView2fcfs.MinValueMarker.Kind = MarkerKind.Circle
        myView2fcfs.MinValueMarker.Size = 10

        myView2fcfs.BarWidth = 0.5

        Fcfsform.fcfsgant.Titles.Add(New ChartTitle())
        Fcfsform.fcfsgant.Titles(0).Text = "Gantt Þemasý"



        Fcfsform.Label2.Text = Pi_toplam
        Fcfsform.fcfsgant.Visible = True
        gecikenis = 0
        For i = 0 To satir - 2
            If cc(i) - dd(i) <= 0 Then Fcfsform.mevcut.Rows(i).Cells(5).Value = (dd(i) - cc(i)) * ee(i)
            If cc(i) - dd(i) > 0 Then Fcfsform.mevcut.Rows(i).Cells(4).Value = (cc(i) - dd(i)) * ll(i)
            If cc(i) - dd(i) > 0 Then gecikenis = gecikenis + 1
        Next
        gecikmee = Fcfsform.mevcut.Rows(0).Cells(4).Value
        Fcfsform.Label10.Text = gecikenis
        For i = 1 To satir - 2
            gecikmee = gecikmee + Fcfsform.mevcut.Rows(i).Cells(4).Value
        Next
        Fcfsform.Label7.Text = gecikmee
        agirliksizgecikme = 0
        For i = 0 To satir - 2

            If cc(i) - dd(i) > 0 Then agirliksizgecikme = agirliksizgecikme + (cc(i) - dd(i))
        Next
        Fcfsform.Label9.Text = agirliksizgecikme


        For i = 0 To satir - 2
            cezaligecikme(i) = Fcfsform.mevcut.Rows(i).Cells(4).Value


        Next i
        For i = 0 To satir - 2

            gecikme(i) = cc(i) - dd(i)
            If gecikme(i) < 0 Then gecikme(i) = 0


        Next i
        For sayaç = 0 To satir - 2




            series2fcfs.Points.Add(New SeriesPoint(noo(sayaç), cezaligecikme(sayaç)))
            series3fcfs.Points.Add(New SeriesPoint(noo(sayaç), gecikme(sayaç)))
        Next
        Fcfsform.fcfsgecikmepie.Series.AddRange(New Series() {series2fcfs})
        Fcfsform.fcfsgecikmepie.Titles.Add(New ChartTitle())
        Fcfsform.fcfsgecikmepie.Titles(0).Text = "Ceza Oranlarý"
        Fcfsform.fcfscezapie.Series.AddRange(New Series() {series3fcfs})
        Fcfsform.fcfscezapie.Titles.Add(New ChartTitle())
        Fcfsform.fcfscezapie.Titles(0).Text = "Gecikme Oranlarý"
        series2fcfs.Label.TextPattern = "{A}: {VP:p0}"
        CType(series2fcfs.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series2fcfs.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
        myview3fcfs.Titles.Add(New SeriesTitle())
        myview3fcfs.Titles(0).Text = series2fcfs.Name
        myview3fcfs.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview3fcfs.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview3fcfs.ExplodeMode = PieExplodeMode.UseFilters
        myview3fcfs.ExplodedDistancePercentage = 30

        CType(series3fcfs.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series3fcfs.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default

        myview4fcfs.Titles.Add(New SeriesTitle())
        myview4fcfs.Titles(0).Text = series3fcfs.Name
        myview4fcfs.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview4fcfs.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview4fcfs.ExplodeMode = PieExplodeMode.UseFilters
        myview4fcfs.ExplodedDistancePercentage = 30

        ' Hide the legend (if necessary).





        satir = BunifuCustomDataGrid1.Rows.Count
        Pi_toplam = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        pp(0) = BunifuCustomDataGrid1.Rows(0).Cells(1).Value
        cc(0) = pp(0)
        For i = 0 To satir - 1
            noo(i) = BunifuCustomDataGrid1.Rows(i).Cells(0).Value
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            dd(i) = BunifuCustomDataGrid1.Rows(i).Cells(2).Value
            ll(i) = BunifuCustomDataGrid1.Rows(i).Cells(3).Value
            ee(i) = BunifuCustomDataGrid1.Rows(i).Cells(4).Value
            ss(i) = pp(i) / ll(i)
        Next i
        For i = 1 To satir - 1
            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value
            cc(i) = cc(i - 1) + pp(i)

        Next

        For i = 1 To satir - 1

            pp(i) = BunifuCustomDataGrid1.Rows(i).Cells(1).Value

            Pi_toplam = Pi_toplam + pp(i)
        Next i

        For sayaç = 0 To satir - 3
            For sayaç2 = sayaç + 1 To satir - 2
                If ss(sayaç2) < ss(sayaç) Then
                    gecici = pp(sayaç)
                    gecici2 = noo(sayaç)
                    gecici3 = dd(sayaç)
                    gecici4 = ee(sayaç)
                    gecici5 = ll(sayaç)
                    gecici6 = cc(sayaç)
                    gecici7 = ss(sayaç)
                    ss(sayaç) = ss(sayaç2)
                    ss(sayaç2) = gecici7
                    cc(sayaç) = cc(sayaç2)
                    cc(sayaç2) = gecici6
                    ll(sayaç) = ll(sayaç2)
                    ll(sayaç2) = gecici5
                    ee(sayaç) = ee(sayaç2)
                    ee(sayaç2) = gecici4
                    dd(sayaç) = dd(sayaç2)
                    dd(sayaç2) = gecici3
                    noo(sayaç) = noo(sayaç2)
                    noo(sayaç2) = gecici2
                    pp(sayaç) = pp(sayaç2)
                    pp(sayaç2) = gecici

                End If
            Next
        Next
        cc(0) = pp(0)
        For i = 1 To satir - 1

            cc(i) = cc(i - 1) + pp(i)

        Next
        series1swpt.ValueScaleType = ScaleType.Numerical

        For sayaç = 0 To satir - 2

            swptform.swpt.Rows.Add(noo(sayaç), pp(sayaç), dd(sayaç), cc(sayaç))



            series1swpt.Points.Add(New SeriesPoint("SWPT", (cc(sayaç) - pp(sayaç)), cc(sayaç)))



        Next


        swptform.swptgant.Series.AddRange(New Series() {series1swpt})
        myView2swpt.Color = Color.Aqua
        myView2swpt.ColorEach = True
        myView2swpt.MaxValueMarkerVisibility = True


        series1swpt.Label.BackColor = Color.Yellow
        series1swpt.Label.LineVisibility = True
        series1swpt.Label.TextColor = Color.Azure
        myView2swpt.MaxValueMarkerVisibility = DefaultBoolean.True
        myView2swpt.MaxValueMarker.Color = Color.GreenYellow
        myView2swpt.MaxValueMarker.Kind = MarkerKind.Star
        myView2swpt.MaxValueMarker.StarPointCount = 5
        myView2swpt.MaxValueMarker.Size = 10

        myView2swpt.MinValueMarkerVisibility = DefaultBoolean.True
        myView2swpt.MinValueMarker.Color = Color.GreenYellow
        myView2swpt.MinValueMarker.Kind = MarkerKind.Circle
        myView2swpt.MinValueMarker.Size = 10

        myView2swpt.BarWidth = 0.5

        swptform.swptgant.Titles.Add(New ChartTitle())
        swptform.swptgant.Titles(0).Text = "Gantt Þemasý"



        swptform.Label2.Text = Pi_toplam

        swptform.swptgant.Visible = True
        gecikenis = 0
        For i = 0 To satir - 2
            If cc(i) - dd(i) <= 0 Then swptform.swpt.Rows(i).Cells(5).Value = (dd(i) - cc(i)) * ee(i)
            If cc(i) - dd(i) > 0 Then swptform.swpt.Rows(i).Cells(4).Value = (cc(i) - dd(i)) * ll(i)
            If cc(i) - dd(i) > 0 Then gecikenis = gecikenis + 1
        Next

        swptform.Label10.Text = gecikenis
        swptform.Label5.Visible = True
        swptform.Label2.Visible = True

        gecikmee = swptform.swpt.Rows(0).Cells(4).Value
        For i = 1 To satir - 2
            gecikmee = gecikmee + swptform.swpt.Rows(i).Cells(4).Value
        Next
        swptform.Label7.Text = gecikmee
        agirliksizgecikme = 0
        For i = 0 To satir - 2

            If cc(i) - dd(i) > 0 Then agirliksizgecikme = agirliksizgecikme + (cc(i) - dd(i))
        Next
        swptform.Label9.Text = agirliksizgecikme


        For i = 0 To satir - 2
            cezaligecikme(i) = swptform.swpt.Rows(i).Cells(4).Value


        Next i
        For i = 0 To satir - 2

            gecikme(i) = cc(i) - dd(i)
            If gecikme(i) < 0 Then gecikme(i) = 0


        Next i
        For sayaç = 0 To satir - 2



            series2swpt.Points.Add(New SeriesPoint(noo(sayaç), cezaligecikme(sayaç)))
            series3swpt.Points.Add(New SeriesPoint(noo(sayaç), gecikme(sayaç)))
        Next
        swptform.swptgecikmepie.Series.AddRange(New Series() {series2swpt})
        swptform.swptgecikmepie.Titles.Add(New ChartTitle())
        swptform.swptgecikmepie.Titles(0).Text = "Ceza Oranlarý"
        swptform.swptcezapie.Series.AddRange(New Series() {series3swpt})
        swptform.swptcezapie.Titles.Add(New ChartTitle())
        swptform.swptcezapie.Titles(0).Text = "Gecikme Oranlarý"
        series2swpt.Label.TextPattern = "{A}: {VP:p0}"
        CType(series2swpt.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series2swpt.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default
        myview3swpt.Titles.Add(New SeriesTitle())
        myview3swpt.Titles(0).Text = series2spt.Name
        myview3swpt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview3swpt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview3swpt.ExplodeMode = PieExplodeMode.UseFilters
        myview3swpt.ExplodedDistancePercentage = 30

        CType(series3swpt.Label, PieSeriesLabel).Position = PieSeriesLabelPosition.TwoColumns
        CType(series3swpt.Label, PieSeriesLabel).ResolveOverlappingMode = ResolveOverlappingMode.Default

        myview4swpt.Titles.Add(New SeriesTitle())
        myview4swpt.Titles(0).Text = series3swpt.Name
        myview4swpt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Value_1, DataFilterCondition.GreaterThanOrEqual, 9))
        myview4swpt.ExplodedPointsFilters.Add(New SeriesPointFilter(SeriesPointKey.Argument, DataFilterCondition.NotEqual, "Others"))
        myview4swpt.ExplodeMode = PieExplodeMode.UseFilters
        myview4swpt.ExplodedDistancePercentage = 30

        ' Hide the legend (if necessary).

        sptform.Hide()
        eddform.Hide()
        swptform.Hide()

        Fcfsform.TopLevel = False 'bilgi formun ya da
        Fcfsform.Dock = DockStyle.Fill
        Panel4.Controls.Add(Fcfsform)
        Fcfsform.Show()
