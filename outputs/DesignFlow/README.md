# DesignFlow

Упрощённая консольная система для автоматизации работы дизайнера. Проект выполнен на C# и .NET 8.

## Возможности

- **Проекты:** создание проекта с названием и клиентом, просмотр списка и изменение статуса.
- **Задачи:** создание задачи для выбранного проекта, просмотр и отметка выполнения.
- **Платежи:** запись платежа с комментарием, просмотр истории и подсчёт суммы по проектам и общего итога.
- **Хранение:** данные сохраняются в `designflow-data.json` рядом с собранным приложением и остаются после перезапуска.

## Запуск

Установите .NET 8 SDK и выполните в папке проекта:

```powershell
dotnet run
```

В основном меню выберите раздел цифрой, затем нужную операцию. Для выхода выберите `0`.

## Структура

- `Program.cs` — меню и взаимодействие с пользователем.
- `Models.cs` — модели проектов, задач и платежей.
- `DataStore.cs` — чтение и сохранение JSON.
- `ProjectService.cs`, `TaskService.cs`, `PaymentService.cs` — логика трёх функций.

## Git и GitHub

Локальный Git-репозиторий проекта находится в папке `ns`, а исходники DesignFlow — в `outputs/DesignFlow`. Для команд Git откройте PowerShell в `C:\Users\Полина\Documents\Codex\2026-10-17\ns`.

```powershell
Set-Location "$env:USERPROFILE\Documents\Codex\2026-10-17\ns"
git config --global user.name "Ваше Имя"
git config --global user.email "ваш-email@example.com"
```

Создайте пустой репозиторий на GitHub, затем подключите его (подставьте адрес своего репозитория):

```powershell
git remote add origin https://github.com/ВАШ-АККАУНТ/DesignFlow.git
git push -u origin master
```

Репозиторий опубликован на GitHub, а пользователь `@cimon-png` принял приглашение. Локально подготовлены ветки `feature/projects`, `feature/tasks`, `feature/payments`, `final`; они уже объединены в `master`. Чтобы опубликовать их вместе с финальным состоянием, выполните:

```powershell
git push origin master feature/projects feature/tasks feature/payments final
```

Второму разработчику нужно клонировать репозиторий в отдельную папку и отправить собственное изменение в отдельной ветке, чтобы показать командную работу:

```powershell
git clone https://github.com/polina17175753/DesignFlow.git DesignFlow-cimon
cd DesignFlow-cimon
git switch -c feature/collaborator
# внесите согласованное изменение в проект
git add .
git commit -m "Изменение от второго разработчика"
git push -u origin feature/collaborator
```
