﻿using CleanBooks.Application.Common.Exceptions;
using CleanBooks.Application.TodoItems.Commands.CreateTodoItem;
using CleanBooks.Application.TodoItems.Commands.DeleteTodoItem;
using CleanBooks.Application.TodoLists.Commands.CreateTodoList;
using CleanBooks.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanBooks.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteTodoItemCommand
        {
            Id = itemId
        });

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
