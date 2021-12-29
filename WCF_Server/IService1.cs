using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCF_Server
{
    // NOTA: è possibile utilizzare il comando "Rinomina" del menu "Refactoring" per modificare il nome di interfaccia "IService1" nel codice e nel file di configurazione contemporaneamente.
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        void DoWork();

        [OperationContract]
        void DoWork2();

        [OperationContract]
        DipendenteServer Login(int id, string pswd);

        [OperationContract]
        ListaProdottiServer getListaProdotti();

        [OperationContract]
        ProdottoServer getProdById(int id);

        [OperationContract]
        List<String> getFreePos();


        [OperationContract]
        bool updateProduct(int id, int quant, string pos, int idDip, string desc, string date);
    }
}
