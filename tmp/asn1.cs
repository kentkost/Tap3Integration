﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.9174
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System.Xml.Serialization;

// 
// This source code was auto-generated by xsd, Version=2.0.50727.3038.
// 


/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.obj-sys.com/v1.0/XMLSchema")]
[System.Xml.Serialization.XmlRootAttribute("DataInterChange", Namespace="http://www.obj-sys.com/v1.0/XMLSchema", IsNullable=false)]
public partial class CharacterString {
    
    private CharacterStringIdentification identificationField;
    
    private string datavaluedescriptorField;
    
    private string stringvalueField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public CharacterStringIdentification identification {
        get {
            return this.identificationField;
        }
        set {
            this.identificationField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("data-value-descriptor", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string datavaluedescriptor {
        get {
            return this.datavaluedescriptorField;
        }
        set {
            this.datavaluedescriptorField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("string-value", Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    public string stringvalue {
        get {
            return this.stringvalueField;
        }
        set {
            this.stringvalueField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.obj-sys.com/v1.0/XMLSchema")]
public partial class CharacterStringIdentification {
    
    private object itemField;
    
    private ItemChoiceType itemElementNameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("context-negotiation", typeof(CharacterStringIdentificationContextnegotiation), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("fixed", typeof(NULL), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("presentation-context-id", typeof(string), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer")]
    [System.Xml.Serialization.XmlElementAttribute("syntax", typeof(string), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="token")]
    [System.Xml.Serialization.XmlElementAttribute("syntaxes", typeof(CharacterStringIdentificationSyntaxes), Form=System.Xml.Schema.XmlSchemaForm.Unqualified)]
    [System.Xml.Serialization.XmlElementAttribute("transfer-syntax", typeof(string), Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="token")]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemElementName")]
    public object Item {
        get {
            return this.itemField;
        }
        set {
            this.itemField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemChoiceType ItemElementName {
        get {
            return this.itemElementNameField;
        }
        set {
            this.itemElementNameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class CharacterStringIdentificationContextnegotiation {
    
    private string presentationcontextidField;
    
    private string transfersyntaxField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("presentation-context-id", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="integer")]
    public string presentationcontextid {
        get {
            return this.presentationcontextidField;
        }
        set {
            this.presentationcontextidField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("transfer-syntax", Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="token")]
    public string transfersyntax {
        get {
            return this.transfersyntaxField;
        }
        set {
            this.transfersyntaxField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.obj-sys.com/v1.0/XMLSchema")]
public partial class NULL {
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true)]
public partial class CharacterStringIdentificationSyntaxes {
    
    private string abstractField;
    
    private string transferField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="token")]
    public string @abstract {
        get {
            return this.abstractField;
        }
        set {
            this.abstractField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Form=System.Xml.Schema.XmlSchemaForm.Unqualified, DataType="token")]
    public string transfer {
        get {
            return this.transferField;
        }
        set {
            this.transferField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://www.obj-sys.com/v1.0/XMLSchema", IncludeInSchema=false)]
public enum ItemChoiceType {
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":context-negotiation")]
    contextnegotiation,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":fixed")]
    @fixed,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":presentation-context-id")]
    presentationcontextid,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":syntax")]
    syntax,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":syntaxes")]
    syntaxes,
    
    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute(":transfer-syntax")]
    transfersyntax,
}
