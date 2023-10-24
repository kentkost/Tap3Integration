﻿using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Formats.Asn1;
using System.Xml;
using Tap0309;
using Tmds.DBus.Protocol;

namespace Tap3Studio.ViewModels;

public partial class MainWindowViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Node> objectNode = new ObservableCollection<Node>();

    [ObservableProperty]
    private string greeting;

    [ObservableProperty]
    private string fuckOff;


    AnyReader reader;
    public MainWindowViewModel()
    {
        reader = new AnyReader();
        reader.PrepareStructures();
        objectNode.Add(reader.Root);
        greeting = "Hello shithead" + reader.Root.TypeName;
        fuckOff = "generate this" + reader.Root.TypeName;
    }

    //public XmlNode RootNode => reader.DataDocument.DocumentElement.FirstChild;

    //public Node ObjectNode => reader.Root;
}