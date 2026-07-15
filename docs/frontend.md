## Компоненты / Дерево

App
├── UploadForm
├── Skeleton - processing
├── Dashboard - success
│ └── Chart (×N)
└── ErrorState - failed

## Состояние

### Серверное

id - TanStack Query
status - TanStack Query
spec - TanStack Query

### Клиентское

form(file + settings) - useState()
