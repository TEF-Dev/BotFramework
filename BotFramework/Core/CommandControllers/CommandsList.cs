﻿using System.Collections.Generic;
using Tef.BotFramework.Abstractions;
using Tef.BotFramework.Common;

namespace Tef.BotFramework.Core.CommandControllers
{
    public class CommandsList
    {
        private bool _caseSensitive = true;
        private Dictionary<string, IBotCommand> _commands = new Dictionary<string, IBotCommand>();

        public CommandsList WithoutCaseSensitive()
        {
            _caseSensitive = false;
            var newCommandList = new Dictionary<string, IBotCommand>();

            foreach (var command in _commands)
                newCommandList.TryAdd(command.Key.ToLower(), command.Value);
            
            _commands = newCommandList;
            return this;
        }

        public void AddCommand(IBotCommand command)
        {
            _commands.TryAdd(_caseSensitive ? command.CommandName : command.CommandName.ToLower(), command);
        }

        public Result<IBotCommand> GetCommand(string commandName)
        {
            return _commands.TryGetValue(commandName, out IBotCommand command)
                ? Result<IBotCommand>.Ok(command)
                : Result<IBotCommand>.Fail($"command {commandName} not founded", null);
        }
    }
}