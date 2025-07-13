<p align="center">
  <img src="images/logo.png" alt="RapidApiCase Logo" width="300" height="100" />
</p>

# 🏨 RapidApiCase (Booking.com Hotel Search & Details App)

This project is a modern web application built with **ASP.NET Core MVC**, leveraging the **Booking.com RapidAPI** to enable users to search for hotels and view detailed information. Users can search by city, dates, and group size, explore results in a sleek interface, and check out hotel details like photos, room options, location, and amenities.

---

## ✨ Key Features

- **Booking.com RapidAPI Integration**  
  - Seamlessly connects to Booking.com’s RapidAPI for hotel searches and details.  
  - Custom response models (`HotelDetailResponse`, `HotelDetail`, `RoomDetail`) handle complex JSON responses.  
  - Maps raw API data to clean ViewModels (`HotelCardViewModel`, `HotelDetailViewModel`, `RoomViewModel`) for display.  

- **Modern & Responsive UI**  
  - Homepage features a user-friendly search form for city, dates, and adult/child counts.  
  - Search results load dynamically via **AJAX** without page refreshes.  
  - Hotel cards are styled with **Bootstrap** and custom CSS for a polished look.  
  - Hotel details page displays the hotel name, logo/photo, rating, address, description, amenities, room options, and an interactive map.  
  - Fully translated to English for a smooth user experience.  

- **Hotel Details & Room Info**  
  - Displays hotel photos, descriptions, ratings, amenities, and room options from Booking.com.  
  - Room cards show details like name, description, photos, price, currency, and max occupancy.  
  - Hotel location is visualized on an interactive **OpenStreetMap** using **Leaflet.js**.  

- **ViewModel & Mapping Logic**  
  - Simplifies bulky API data into lightweight ViewModels, filtering out unused fields.  
  - Centralizes mapping logic in the `BookingService` for clean, maintainable code.  

- **Code & Design Optimizations**  
  - Reduced code repetition and complexity for a streamlined codebase.  
  - Made properties nullable to handle missing API data gracefully.  
  - Designed a clean, responsive UI with a simplified photo gallery (main photo under hotel name).  

- **Extra Features**  
  - Supports additional details like hotel amenities, spoken languages, and Booking.com badges, ready for display.  
  - Built flexibly to accommodate new API fields or themes.  

---

## 🧩 Architecture

### 🔹 Layers

- **Controllers**: Handle user requests and interact with services.  
- **Services**: Encapsulate API calls and data mapping logic (e.g., `BookingService`).  
- **ViewModels**: Simplify data transfer between API responses and views (`HotelCardViewModel`, `HotelDetailViewModel`, etc.).  
- **Views**: Built with Razor Pages for a dynamic, user-friendly interface.  
- **Partials/Components**: Reusable UI elements for hotel cards, search forms, and photo galleries.  

### 🔹 Data Flow

- The app fetches data from Booking.com’s RapidAPI.  
- Complex JSON responses are parsed into custom response models.  
- Data is filtered and mapped to ViewModels for display.  
- **OpenStreetMap** (via Leaflet.js) is used for location visualization.  

---

## 🧪 Technologies Used

- ASP.NET Core MVC  
- Booking.com RapidAPI  
- Bootstrap 5  
- Leaflet.js (OpenStreetMap)  
- JavaScript (AJAX - Fetch API)  
- C# 10  
- LINQ, Async/Await, ViewModel Pattern  

---

## 📸 Application Screenshots

### 🏠 Homepage with Hotel Search Form
| ![Homepage](images/homepage.png) |
|:--------------------------------:|
| The homepage features a clean search form for city, dates, and group size. |

### 🔍 Hotel Search Results (Hotel Cards)
| ![Search Results](images/search-results.png) |
|:--------------------------------------------:|
| Hotel cards display key info with a modern, responsive design. |

### 🏨 Hotel Details Main View
| ![Hotel Details](images/hotel-details.png) |
|:------------------------------------------:|
| Shows hotel name, rating, address, description, and amenities. |

### 🛏️ Room Images
| ![Room Images](images/room-images.png) |
|:--------------------------------------:|
| Displays room details with photos, prices, and descriptions. |

### 📍 Hotel Location
| ![Hotel Location](images/hotel-location.png) |
|:--------------------------------------------:|
| Interactive map powered by OpenStreetMap and Leaflet.js. |

---
