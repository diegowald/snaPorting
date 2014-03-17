Option Explicit On
Public Module Enumerados

    ' Define como se llama este modulo para el control de errores
    Private Const m_sMODULENAME_ As String = "MisEnumeraciones"

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

    Public Enum LosObjetos
        VVisita = 1
        PPedido = 2
        RRecibo = 3
        DDevolucion = 4
        VVisor = 5
        IInterDeposito = 6
        RRendicion = 7
    End Enum


End Module
