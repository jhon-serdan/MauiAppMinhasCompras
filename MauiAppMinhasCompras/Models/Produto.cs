﻿using SQLite;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        double _quantidade;
        double _preco;
        string _categoria;

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {

            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }

                _descricao = value;
            }
        }
        public double Quantidade
        {
            get => _quantidade;
            set
            {
                if (value == 0)
                {
                    throw new Exception("Por favor, preencha a quantidade diferente de 0");
                }

                _quantidade = value;
            }
        }
        public double Preco
        {
            get => _preco;
            set
            {
                if (value == 0)
                {
                    throw new Exception("Por favor, preencha o valor diferente de 0");
                }

                _preco = value;
            }
        }
        public double Total { get => Quantidade * Preco; }

        public string Categoria
        {

            get => _categoria;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a categoria");
                }

                _categoria = value;
            }
        }
    }
}
