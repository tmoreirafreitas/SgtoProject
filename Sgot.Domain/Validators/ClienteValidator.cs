using FluentValidation;
using Sgot.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sgot.Domain.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(c => c).NotNull().OnAnyFailure(cl =>
              {
                  throw new ArgumentNullException("O objeto cliente não pode ser nulo.");
              });

            RuleFor(c => c.Cpf).NotEmpty().WithMessage("É necessário informar o CPF");
            RuleFor(c => c.Email).NotEmpty().WithMessage("É necessário informar o E-mail");
            RuleFor(c => c.Nascimento).NotNull().WithMessage("É necessário informar a data de nascimento");
            RuleFor(c => c.Nome).NotEmpty().WithMessage("É necessário informar o nome");
            RuleFor(c => c.Rg).NotEmpty().WithMessage("É necessário informar o RG");
            RuleFor(c => c.Telefone).NotEmpty().WithMessage("É necessário informar o telefone de contato");

            RuleFor(e => e.Cep).NotEmpty().WithMessage("É necessário informar o CEP");
            RuleFor(e => e.Bairro).NotEmpty().WithMessage("É necessário informar o Bairro");
            RuleFor(e => e.Cidade).NotEmpty().WithMessage("É necessário informar a Cidade");
            RuleFor(e => e.Estado).NotEmpty().WithMessage("É necessário informar o Estado");
        }
    }
}
