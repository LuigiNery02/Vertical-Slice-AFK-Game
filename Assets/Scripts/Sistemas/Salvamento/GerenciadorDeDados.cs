using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class GerenciadorDeDados
{
    private string _caminhoDoArquivo = ""; //caminho onde o arquivo de save será salvado

    private string _nomeDoArquivo = ""; //nome do arquivo de save

    private bool _encriptar = false; //variável para verificar se o arquivo deve ser encriptado

    private readonly string _codigoDeEncriptacao = "word"; //código de encriptação

    public GerenciadorDeDados(string caminho, string arquivo, bool encriptar)
    {
        //define as variáveis
        this._caminhoDoArquivo = caminho;
        this._nomeDoArquivo = arquivo;
        this._encriptar = encriptar;
    }

    public GameData Carregar(string ID) //carregar seus dados específicos
    {
        string caminhoCompleto = Path.Combine(_caminhoDoArquivo, ID, _nomeDoArquivo); //caminho completo do arquivo do save
        GameData saveCarregado = null;
        if(File.Exists(caminhoCompleto)) //se encontrar o caminho
        {
            try
            {
                string saveParaCarregar = "";

                using(FileStream fluxo = new FileStream(caminhoCompleto, FileMode.Open))
                {
                    using (StreamReader leitor = new StreamReader(fluxo))
                    {
                        saveParaCarregar = leitor.ReadToEnd();
                    }
                }

                if(_encriptar)
                {
                    saveParaCarregar = VerificarEncriptacao(saveParaCarregar);
                }

                saveCarregado = JsonUtility.FromJson<GameData>(saveParaCarregar);
            }
            catch(Exception e)
            {
                Debug.LogError("Erro ocorrido ao tentar carregar os dados para o caminho:" + caminhoCompleto + "\n" + e);
            }
        }
        return saveCarregado;
    }

    public void Salvar(GameData save, string ID) //salvar seus dados específicos
    {
        string caminhoCompleto = Path.Combine(_caminhoDoArquivo, ID, _nomeDoArquivo); //caminho completo do arquivo do save
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(caminhoCompleto));

            string dataToStore = JsonUtility.ToJson(save, true);

            if (_encriptar)
            {
                dataToStore = VerificarEncriptacao(dataToStore);
            }

            using(FileStream fluxo = new FileStream(caminhoCompleto, FileMode.Create))
            {
                using(StreamWriter escritor = new StreamWriter(fluxo))
                {
                    escritor.Write(dataToStore);
                }
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Erro ocorrido ao tentar salvar os dados para o caminho:" + caminhoCompleto + "\n" + e);
        }
    }

    public void Deletar(string ID) //deletar seus dados específicos
    {
        if(ID == null)
        {
            return;
        }

        string caminhoCompleto = Path.Combine(_caminhoDoArquivo, ID, _nomeDoArquivo);  //caminho completo do arquivo do save
        try
        {
            if(File.Exists(caminhoCompleto)) //se encontrar o caminho
            {
                Directory.Delete(Path.GetDirectoryName(caminhoCompleto), true); //deleta o caminho
            }
            else
            {
                Debug.LogWarning("Erro ao tentar deletar" + caminhoCompleto);
            }
        }
        catch(Exception e)
        {
            Debug.LogError("Falha ao tentar deletar" + ID + "no caminho" + caminhoCompleto + "\n" + e);
        }
    }

    public Dictionary<string, GameData> CarregarTodosOsDados()
    {
        Dictionary<string, GameData> perfil = new Dictionary<string, GameData>();

        IEnumerable<DirectoryInfo> informacoes = new DirectoryInfo(_caminhoDoArquivo).EnumerateDirectories();
        foreach(DirectoryInfo info in informacoes)
        {
            string perfilID = info.Name;

            string fullPath = Path.Combine(_caminhoDoArquivo, perfilID, _nomeDoArquivo);
            if (!File.Exists(fullPath))
            {
                continue;
            }

            GameData profileData = Carregar(perfilID);

            if(profileData != null)
            {
                perfil.Add(perfilID, profileData);
            }
            else
            {
                Debug.LogError("Erro ao tentar carregar ProfileId:" + perfilID);
            }
        }

        return perfil;
    }

    private string VerificarEncriptacao(string data)
    {
        string saveModificado = "";
        for(int i = 0; i < data.Length; i++)
        {
            saveModificado += (char)(data[i] ^ _codigoDeEncriptacao[i % _codigoDeEncriptacao.Length]);
        }
        return saveModificado;
    }
}
