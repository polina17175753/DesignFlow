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

Локальный Git-репозиторий проекта находится в папке `ns`, а исходники DesignFlow — в `outputs/DesignFlow`. Поэтому для команд Git откройте PowerShell в `C:\Users\Полина\Documents\Codex\2026-10-17\ns` (или перейдите туда командой ниже). Начальный коммит уже создан. Если настраиваете проект на другом компьютере, начните с `git init -b master` и начального коммита.

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

Добавьте одногруппника в **Settings → Collaborators**. Для демонстрации командной разработки создайте рабочие ветки, вносите изменения и фиксируйте их коммитами:

```powershell
git switch -c feature/projects
git add .
git commit -m "Добавлено управление проектами"
git push -u origin feature/projects

git switch master
git switch -c feature/tasks
# внесите отдельное изменение в функциональность задач
git add .
git commit -m "Добавлено управление задачами"
git push -u origin feature/tasks

git switch master
git switch -c feature/payments
# внесите отдельное изменение в функциональность платежей
git add .
git commit -m "Добавлен учёт платежей"
git push -u origin feature/payments

git switch master
git pull
git switch -c final
git merge feature/projects
git merge feature/tasks
git merge feature/payments
git switch master
git merge final
git push origin master
```

Начальный локальный коммит подготовлен в ветке `master`. Публикация на GitHub, приглашение одногруппника и функциональные коммиты в трёх ветках ещё не выполнены: для этого нужны созданный удалённый репозиторий и участие второго разработчика.
