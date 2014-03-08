Option Explicit On
Public Module Enumerados

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "MisEnumeraciones"

    Public Enum LosSabores
        A1_Todos = 1
        A2_Clientes = 2
        A3_Viajantes = 3
        A4_Administrador = 4
    End Enum


#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
Public A1_Todos, A2_Clientes, A3_Viajantes, A4_Administrador
#End If

    ' Necesarios para llenar mas rapido el list view
    Public Enum TreeCategory
        Marca = 0
        Familia = 1
        Linea = 2
        Nuevo = 3
        Oferta = 4
        Alertas = 5
    End Enum
#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
Public Marca, Familia, Linea, Nuevo, Oferta
#End If

    ' Objetos que se Auditan
    Public Enum ObjetosAuditados
        RegistroPrograma = 1
        Logueo = 2
        ActualizacionCliente = 3
        ActualizacionCatalogo = 4
        Programa = 5
        Visita = 6
        Pedido = 7
        ImpresionPedido = 8
        Recibo = 9
        ImpresionRecibo = 10
        Seguridad = 11
        AppConfig = 12
        Comandos = 13
        WebServices = 14
        Comunicaciones = 15
        TiempoOperacion = 16
        SubRutina = 17
        Devoluciones = 18
        ImpresionDevolucion = 19
        ActualizacionGeneral = 20
        InterDeposito = 21
        ImpresionInterDeposito = 22
        Rendicion = 23
        ImpresionRendicion = 24
        ErrordePrograma = 99
        Rutina = 100
    End Enum

#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
Public RegistroPrograma, Logueo, ActualizacionCliente, ActualizacionCatalogo, Programa
Public Visita, Pedido, ImpresionPedido, Recibo, ImpresionRecibo, ImpresionDevolucion, ImpresionInterDeposito, ImpresionRendicion
Public Seguridad, AppConfig, Comandos, WebServices, Comunicaciones, TiempoOperacion, ErrordePrograma, Rutina
Public SubRutina, Devoluciones, ActualizacionGeneral, InterDeposito, Rendicion
#End If

    ' Acciones Auditadas
    Public Enum AccionesAuditadas
        INICIA = 1
        EXITOSO = 2
        FALLO = 3
        CANCELA = 4
        GUARDA = 5
        TRANSMITE = 6
        IMPRIME = 7
        INFORMA = 8
        TERMINA = 10
        DESCONOCIDO = 100
    End Enum

#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
Public INICIA, EXITOSO, FALLO, CANCELA, GUARDA, TRANSMITE
Public IMPRIME, INFORMA, TERMINA, DESCONOCIDO
#End If

    '#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
    '    Public cCodigo, cLinea, cPrecio, cFamilia, cMarca, cModelo, cDescripcion, cMotor, cAño, cMedidas, cReemplazaA
    '    Public cContiene, cEquivalencia, cOriginal, cPlista, cPoferta, cRotacion, cEvolucion, cPorclinea
    '    Public cId, cControl, cCodigoAns, cMiCodigo, cE_bhi, cE_bsa, cE_mdp, cE_mza, cE_ros, cSuspendido, cOfertaCantidad
    '#End If

    Public Enum LosObjetos
        VVisita = 1
        PPedido = 2
        RRecibo = 3
        DDevolucion = 4
        VVisor = 5
        IInterDeposito = 6
        RRendicion = 7
    End Enum

#If False Then ' Truco para preservar formato del enum cuando se tipea en el IDE
Public VVisita, PPedido, RRecibo, DDevolucion, VVisor, IInterDeposito, RRendicion
#End If


End Module
